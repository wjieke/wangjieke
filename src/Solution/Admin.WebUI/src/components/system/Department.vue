<template>
    <!--外容器-->
    <el-container>
        <!--上面层-->
        <el-header>
            <!--搜索栏-->
            <el-card class="search-card">
                <el-form :inline="true" :model="searchModel" class="search-form-inline">
                    <el-form-item label="名称">
                        <el-input v-model="searchModel.departmentName" placeholder="名称" clearable></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" icon="el-icon-search" @click="search">查询</el-button>
                    </el-form-item>
                </el-form>
            </el-card>
        </el-header>
        <!--下面层-->
        <el-container>
            <!--左侧栏-->
            <el-aside>
                <el-card class="tree-card">
                    <el-tree class="filter-tree"
                             :data="trees"
                             :props="treeProps"
                             default-expand-all
                             :expand-on-click-node="false"
                             :filter-node-method="filterNode"
                             ref="tree"
                             @node-click="handleNodeClick">
                    </el-tree>
                </el-card>
            </el-aside>
            <!--右侧栏-->
            <el-main>
                <!--操作栏-->
                <el-card class="operation-card">
                    <!--面包屑-->
                    <el-breadcrumb separator="/">
                        <el-breadcrumb-item v-for="(o,index) in paths" :key="index" @click.native.prevent="onLinkObjectData(o.id)">
                            <a href="#">{{o.departmentName}}</a>
                        </el-breadcrumb-item>
                    </el-breadcrumb>
                    <!--按钮组-->
                    <el-button-group>
                        <el-button type="primary" icon="el-icon-circle-plus-outline" @click="add">添加</el-button>
                        <el-button type="primary" icon="el-icon-edit" @click="mod">修改</el-button>
                        <el-button type="primary" icon="el-icon-delete" @click="del">删除</el-button>
                    </el-button-group>
                </el-card>
                <!--表格栏-->
                <el-card class="table-card">
                    <el-table :data="datas"
                              style="width: 100%"
                              max-height="424"
                              stripe :default-sort="{prop: 'id', order: 'descending'}"
                              @selection-change="handleSelectionChange">
                        <el-table-column fixed type="selection" width="55"></el-table-column>
                        <el-table-column fixed prop="id" label="ID" width="120" sortable></el-table-column>
                        <el-table-column fixed prop="departmentName" label="部门名称" show-overflow-tooltip width="180">
                            <template slot-scope="scope">
                                <el-button type="text" size="small" @click="getListById(scope.row)">{{ scope.row.departmentName }}</el-button>
                            </template>
                        </el-table-column>
                        <el-table-column prop="sort" label="排序"></el-table-column>
                        <el-table-column prop="remark" label="备注"></el-table-column>
                        <el-table-column prop="modifyTime" label="修改时间" width="160"></el-table-column>
                        <el-table-column prop="modifierId" label="修改者ID"></el-table-column>
                        <el-table-column prop="createTime" label="创建时间" width="160"></el-table-column>
                        <el-table-column prop="creatorId" label="创建者ID"></el-table-column>
                        <el-table-column prop="dataState" label="数据状态" fixed="right">
                            <template slot-scope="scope">
                                <span v-html="scope.row.dataState"></span>
                            </template>
                        </el-table-column>
                    </el-table>
                </el-card>
                <!--分页栏-->
                <el-card class="pagination-card">
                    <el-pagination @size-change="handleSizeChange"
                                   @current-change="handleCurrentChange"
                                   :current-page="pagingModel.currentPage"
                                   :page-sizes="pagingModel.pageSizes"
                                   :page-size="pagingModel.pageSize"
                                   :pager-count="pagingModel.pagerCount"
                                   layout="total, sizes, prev, pager, next, jumper"
                                   :total="pagingModel.total">
                    </el-pagination>
                </el-card>
            </el-main>
        </el-container>
        <!--弹出框-->
        <el-dialog title="部门信息" :visible.sync="dialogFormVisible">
            <el-form :inline="false" :model="entityModel" :rules="rules" ref="editForm" label-width="80px">
                <el-row :gutter="20">
                    <el-col :span="24">
                        <el-form-item label="部门名称" prop="departmentName">
                            <el-input placeholder="名称" v-model="entityModel.departmentName" autocomplete="off" clearable></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row style="margin-top:20px;">
                    <el-col :span="24">
                        <el-form-item label="部门排序" prop="sort">
                            <el-input-number placeholder="排序" v-model="entityModel.sort" :min="0" :max="10" label="排序" style="width:100%;"></el-input-number>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row style="margin-top:20px;">
                    <el-col :span="24">
                        <el-form-item label="所属部门">
                            <el-cascader style="width:100%" :options="nodes"
                                         v-model="entityModel.ids"
                                         change-on-select
                                         filterable
                                         :clearable="true"
                                         :props="nodeProps"
                                         @change="handleItemChange"
                                         expand-trigger="hover">
                            </el-cascader>
                        </el-form-item>
                    </el-col>
                </el-row>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogFormVisible = false">取 消</el-button>
                <el-button @click="resetForm('editForm')">重置</el-button>
                <el-button type="primary" @click="sava('editForm')">确 定</el-button>
            </div>
        </el-dialog>
    </el-container>
</template>

<script>
    import { timeFormat } from "@/js/common/utility.js";
    import { paginationModel as pagingModel } from '@/js/common/pagination';
    import { departmentUrl } from "@/js/common/api.js";
    export default {
        name: 'DepartmentTemplate',
        data() {
            return {
                pagingModel,
                searchModel: {
                    parentId: null,
                    departmentName: ''
                },
                entityModel: {
                    id: 0,
                    parentId: null,
                    ids: [],
                    departmentName: '',
                    sort: 0
                },
                rules: {
                    departmentName: [
                        { required: true, message: '请输入名称', trigger: 'blur' }
                    ]
                },
                treeProps: {
                    id: 'id',
                    label: 'departmentName',
                    children: 'children'
                },
                nodeProps: {
                    value: 'id',
                    label: 'departmentName',
                    children: 'children',
                    disabled: 'disabled',
                    expandTrigger: 'hover',
                    checkStrictly: 'false'
                },
                trees: [],
                nodes: [],
                datas: [],
                paths: [],
                multipleSelection: [],
                inputIsDisabled: false,
                dialogFormVisible: false,
                //筛选文本，过滤树控件的检索文本
                filterText: ''
            }
        },
        components: {

        },
        created: function () {

        },
        mounted: function () {
            this.getTree();
            this.getNode();
            this.getPath();
            this.getPage();
        },
        computed: {

        },
        methods: {
            //添加
            add() {
                this.entityModel.id = 0;
                this.entityModel.parentId = null;
                this.entityModel.ids = [];
                this.entityModel.departmentName = '';
                this.entityModel.sort = 0;
                this.dialogFormVisible = true;
            },
            //修改
            mod() {
                var _this = this;
                var ids = this.getIds();
                if (ids.length === 0 || ids.length > 1) {
                    this.$message({
                        type: 'info',
                        message: '请选择一条数据修改'
                    });
                    return;
                }
                this.$axios({
                    method: 'get',
                    url: departmentUrl.getInfo,
                    params: { id: ids[0] }
                }).then(function (response) {
                    _this.entityModel = response.data.data;
                    _this.dialogFormVisible = true;
                });
            },
            //删除
            del() {
                var _this = this;
                var ids = _this.getIds();
                if (ids.length === 0) {
                    this.$message({
                        type: 'info',
                        message: '请至少选择一条数据删除'
                    });
                    return;
                }
                this.$confirm('确认删除该数据吗, 是否继续?', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                }).then(() => {
                    _this.$axios({
                        method: 'delete',
                        url: departmentUrl.delInfo,
                        data: ids
                    }).then(function (response) {
                        if (response.data.resultState === 1) {
                            _this.$message({
                                showClose: true,
                                message: '提示，删除成功',
                                type: 'success',
                                duration: 1000,
                                onClose: function () {
                                    _this.getPage();
                                }
                            });
                        } else {
                            _this.$message({
                                type: 'info',
                                message: '删除失败！' + response.data.message
                            });
                        }
                    });
                }).catch(() => {
                    this.$message({
                        type: 'info',
                        message: '已取消删除'
                    });
                });
            },
            //保存
            sava(formName) {
                var ids = this.entityModel.ids;
                var parentId = ids[ids.length - 1];
                if (parentId !== this.entityModel.id) {
                    this.entityModel.parentId = parentId;
                }
                var _this = this;
                var apiUrl = '';
                var requestMethod = '';
                if (this.entityModel.id === 0) {
                    //添加
                    apiUrl = departmentUrl.addInfo;
                    requestMethod = 'post';
                } else {
                    //修改
                    apiUrl = departmentUrl.modInfo;
                    requestMethod = 'put';
                }
                //验证表单
                this.$refs[formName].validate((valid) => {
                    if (valid) {
                        _this.$axios({
                            method: requestMethod,
                            url: apiUrl,
                            data: _this.entityModel
                        }).then(function (response) {
                            if (response.data.resultState === 1) {
                                _this.$message({
                                    showClose: true,
                                    message: '提示，保存成功',
                                    type: 'success',
                                    duration: 1000,
                                    onClose: function () {
                                        //关闭dialog窗口
                                        //_this.dialogFormVisible = false;
                                        //刷新表格数据列表
                                        _this.getPage();
                                        //刷新树控件
                                        _this.getTree();
                                        //清空名称
                                        _this.entityModel.departmentName = '';
                                    }
                                });
                            } else {
                                _this.$message({
                                    type: 'info',
                                    message: '失败！' + response.data.message
                                });
                            }
                        });
                    } else {
                        //console.log('error submit!! 验证失败');
                        return false;
                    }
                });
            },
            //搜索
            search() {
                this.pagingModel.currentPage = 1;
                this.getPage();
            },
            //重置
            resetForm(formName) {
                this.$refs[formName].resetFields();
            },
            //绑定表格数据（获取数据列表）
            getPage() {
                var _this = this;
                var searchData = {
                    pageIndex: this.pagingModel.currentPage,
                    pageSize: this.pagingModel.pageSize,
                    parentId: this.searchModel.parentId,
                    departmentName: this.searchModel.departmentName
                };
                _this.$axios({
                    method: 'post',
                    url: departmentUrl.getPage,
                    data: searchData
                }).then(function (response) {
                    var retDatas = response.data;
                    if (retDatas.datas != null) {
                        retDatas.datas.forEach(obj => {
                            obj.createTime !== null ? obj.createTime = timeFormat(obj.createTime) : obj.createTime = null;
                            obj.modifyTime !== null ? obj.modifyTime = timeFormat(obj.modifyTime) : obj.modifyTime = null;
                            switch (obj.dataState) {
                                case 1:
                                    obj.dataState = '<span style="color:green;">启用</span>'
                                    break;
                                case 2:
                                    obj.dataState = '<span style="color:gray;">停用</span>'
                                    break;
                                default:
                            }
                        });
                    } else { retDatas.datas = []; }
                    _this.datas = retDatas.datas;
                    _this.pagingModel.total = retDatas.total;
                });
            },
            //获取多个id字符串
            getIds() {
                var items = this.multipleSelection;
                var ids = [];
                items.map(function (item) { ids.push(item.id); })
                return ids;
            },
            //绑定级联器
            getNode() {
                var _this = this;
                _this.$axios({
                    method: 'get',
                    url: departmentUrl.getNode,
                    params: { id: null }
                }).then(function (response) {
                    var retDatas = response.data;
                    _this.nodes = retDatas;
                });
            },
            //绑定树
            getTree() {
                var _this = this;
                _this.$axios({
                    method: 'get',
                    url: departmentUrl.getTree
                    //params: { id: null }
                }).then(function (response) {
                    var retDatas = response.data;
                    _this.trees = retDatas;
                });
            },
            //获取路径
            getPath() {
                var _this = this;
                _this.$axios({
                    method: 'get',
                    url: departmentUrl.getPath,
                    params: { id: _this.searchModel.parentId }
                }).then(function (response) {
                    _this.paths = response.data;
                });
            },
            //val页大小(每页多少条)
            handleSizeChange(val) {
                this.pagingModel.pageSize = val;
            },
            //val页索引(当前第几页)
            handleCurrentChange(val) {
                this.pagingModel.currentPage = val;
            },
            //多选
            handleSelectionChange(val) {
                this.multipleSelection = val;
            },
            //cascader级联选择器选中改变事件
            handleItemChange(val) {
                if (val.length > 0) {
                    this.inputIsDisabled = true;
                } else {
                    this.inputIsDisabled = false;
                }
            },
            //根据父ID获取对象数据列表
            getListById(row) {
                var id = row.id;
                this.searchModel.parentId = id;
                this.pagingModel.currentPage = 1;
                this.getPage();
                this.getPath();
            },
            //链接对象数据
            onLinkObjectData(parentId) {
                if (parentId === 0) { parentId = null; }
                this.searchModel.parentId = parentId;
                this.searchModel.departmentName = "";
                this.getPage();
                this.getPath();
            },
            //点击树节点执行
            handleNodeClick(data) {
                this.onLinkObjectData(data.id);
            },
            //树节点文本过滤
            filterNode(value, data) {
                if (!value) return true;
                return data.departmentName.indexOf(value) !== -1;
            }
        },
        watch: {
            pagingModel: {
                deep: true,
                handler: function () {
                    this.getPage();
                }
            },
            //监测搜索文本过滤树控件数据
            filterText(val) {
                this.$refs.tree.filter(val);
            }
        }
    }
</script>

<style scoped>
    .el-header {
        height: 124px !important;
    }

    .search-card, .operation-card, .pagination-card {
        text-align: left;
    }

    .search-form-inline {
        margin-top: 20px;
    }

    .el-aside {
        padding-left: 20px;
        max-height: 650px;
    }

    .el-main {
        padding-left: 0px;
        padding-top: 0px;
    }

    .el-button-group {
        margin-top: 15px;
    }
</style>