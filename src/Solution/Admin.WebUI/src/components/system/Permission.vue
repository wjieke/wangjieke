<template>
    <!--外容器-->
    <el-container>
        <!--上面层-->
        <el-header>
            <!--搜索栏-->
            <el-card class="search-card">
                <el-form :inline="true" :model="searchModel" class="search-form-inline">
                    <el-form-item label="关键字">
                        <el-input v-model="searchModel.permissionName" placeholder="名称" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="终端类型">
                        <el-select v-model="searchModel.clientCategory" placeholder="请选择">
                            <el-option v-for="item in searchModel.clientCategorys"
                                       :key="item.value"
                                       :label="item.label"
                                       :value="item.value">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item label="数据状态">
                        <el-select v-model="searchModel.dataState" placeholder="请选择">
                            <el-option v-for="item in searchModel.dataStates"
                                       :key="item.value"
                                       :label="item.label"
                                       :value="item.value">
                            </el-option>
                        </el-select>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" icon="el-icon-search" @click="search">搜索</el-button>
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
                        <el-breadcrumb-item v-for="(o,index) in paths" :key="index" @click.native.prevent="onLinkObjectData(o.permissionId)">
                            <a href="#">{{o.permissionName}}</a>
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
                              style="width: 100%;"
                              max-height="424"
                              stripe :default-sort="{prop: 'permissionId', order: 'descending'}"
                              @selection-change="handleSelectionChange">
                        <el-table-column fixed type="selection" width="55"></el-table-column>
                        <el-table-column prop="permissionId" label="权限编号" width="200" sortable></el-table-column>
                        <el-table-column prop="permissionName" label="权限名称" show-overflow-tooltip width="">
                            <template slot-scope="scope">
                                <el-button type="text" size="small" @click="getObjectDataById(scope.row)">{{ scope.row.permissionName }}</el-button>
                            </template>
                        </el-table-column>
                        <el-table-column prop="clientCategory" label="终端类别" width="120" sortable></el-table-column>
                        <el-table-column prop="sort" label="排序" width="120" sortable></el-table-column>
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
        <el-dialog title="权限信息" :visible.sync="addOrModDialogFormVisible">
            <el-form :inline="false" :model="entityModel" :rules="rules" ref="editForm" label-width="80px">
                <el-row :gutter="20">
                    <el-col :span="12">
                        <el-form-item label="权限编号" prop="permissionId">
                            <el-input placeholder="编号" v-model="entityModel.permissionId" autocomplete="off" clearable style="width:260px;"></el-input>
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item label="权限名称" prop="permissionName">
                            <el-input placeholder="名称" v-model="entityModel.permissionName" autocomplete="off" clearable style="width:260px;"></el-input>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row :gutter="20" style="margin-top:20px;">
                    <el-col :span="12">
                        <el-form-item label="终端类别">
                            <el-select v-model="entityModel.clientCategory" placeholder="请选择" style="width:260px;">
                                <el-option v-for="item in entityModel.clientCategorys"
                                           :key="item.value"
                                           :label="item.label"
                                           :value="item.value">
                                </el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item label="所属权限">
                            <el-cascader :options="nodes"
                                         v-model="entityModel.ids"
                                         filterable
                                         :clearable="true"
                                         :props="nodeProps"
                                         style="width:260px;"
                                         @change="handleItemChange">
                            </el-cascader>
                        </el-form-item>
                    </el-col>
                </el-row>
                <el-row :gutter="20" style="margin-top:20px;">
                    <el-col :span="12">
                        <el-form-item label="权限排序">
                            <el-input-number v-model="entityModel.sort" :min="1" label="" style="width:260px;"></el-input-number>
                        </el-form-item>
                    </el-col>
                    <el-col :span="12">
                        <el-form-item label="权限状态">
                            <el-select v-model="entityModel.dataState" placeholder="请选择" style="width:260px;">
                                <el-option v-for="item in entityModel.dataStates"
                                           :key="item.value"
                                           :label="item.label"
                                           :value="item.value">
                                </el-option>
                            </el-select>
                        </el-form-item>
                    </el-col>
                </el-row>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="addOrModDialogFormVisible = false">取 消</el-button>
                <el-button @click="resetForm('editForm')">重置</el-button>
                <el-button type="primary" @click="sava('editForm')">确 定</el-button>
            </div>
        </el-dialog>
    </el-container>
</template>

<script>
    import { timeFormat } from "@/js/common/utility.js";
    import { paginationModel as pagingModel } from '@/js/common/pagination';
    import { enumUrl, permissionUrl } from "@/js/common/api.js";
    export default {
        name: 'PermissionTemplate',
        data() {
            return {
                pagingModel,
                searchModel: {
                    parentId: null,
                    permissionName: '',
                    dataState: 0,
                    clientCategory: 0,
                    dataStates: [],
                    clientCategorys: []
                },
                entityModel: {
                    permissionId: '',
                    parentId: null,
                    ids: [],
                    permissionName: '',
                    sort: 0,
                    dataState: 0,
                    clientCategory: 0,
                    dataStates: [],
                    clientCategorys: []
                },
                rules: {
                    permissionName: [
                        { required: true, message: '请输入名称', trigger: 'blur' }
                    ]
                },
                treeProps: {
                    id: 'permissionId',
                    label: 'permissionName',
                    children: 'children'
                },
                nodeProps: {
                    value: 'permissionId',
                    label: 'permissionName',
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
                addOrModDialogFormVisible: false,
                filterText: '',
                isEdit: false
            }
        },
        components: {

        },
        created: function () {
            this.getPage();
            this.getPath();
            this.getTree();
            this.getNode();
            this.getDataState();
            this.getClientCategory();
        },
        mounted: function () {

        },
        computed: {

        },
        methods: {
            //#region 增删改

            //添加
            add() {
                this.entityModel.permissionId = '';
                this.entityModel.parentId = null;
                this.entityModel.ids = [];
                this.entityModel.permissionName = '';
                this.entityModel.sort = 0;
                this.entityModel.clientCategory = 1;
                this.entityModel.dataState = 1;
                this.addOrModDialogFormVisible = true;

                this.isEdit = false;
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
                    url: permissionUrl.getInfo,
                    params: { id: ids[0] }
                }).then(function (response) {
                    _this.entityModel = response.data.data;
                    _this.isEdit = true;
                    _this.addOrModDialogFormVisible = true;
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
                        url: permissionUrl.delInfo,
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

            //#endregion

            //#region 数据初始化
            getClientCategory() {
                var _this = this;
                //终端类型
                _this.$axios({
                    method: 'get',
                    url: enumUrl.getClientCategory
                }).then(function (response) {
                    _this.searchModel.clientCategorys = response.data;
                    _this.searchModel.clientCategory = response.data[0].value;
                });
            },
            getDataState() {
                var _this = this;
                //数据状态
                _this.$axios({
                    method: 'get',
                    url: enumUrl.getDataState
                }).then(function (response) {
                    _this.searchModel.dataStates = response.data;
                    _this.searchModel.dataState = response.data[0].value;
                });
            },
            getTree() {
                var _this = this;
                //所属权限
                _this.$axios({
                    method: 'post',
                    url: permissionUrl.getTree,
                    params: { parentId: null }
                }).then(function (response) {
                    _this.trees = response.data;
                });
            },
            //添加编辑页面数据初始化
            getNode() {
                var _this = this;
                //所属权限
                _this.$axios({
                    method: 'post',
                    url: permissionUrl.getNode,
                    params: { parentId: null }
                }).then(function (response) {
                    _this.nodes = response.data;
                });
            },
            //获取路径
            getPath() {
                var _this = this;
                _this.$axios({
                    method: 'post',
                    url: permissionUrl.getPath,
                    params: { id: _this.searchModel.parentId }
                }).then(function (response) {
                    _this.paths = response.data;
                });
            },
            //绑定表格数据（获取数据列表）
            getPage() {
                var _this = this;
                var searchModel = {
                    pageIndex: this.pagingModel.currentPage,
                    pageSize: this.pagingModel.pageSize,
                    parentId: this.searchModel.parentId,
                    permissionName: this.searchModel.permissionName
                };
                _this.$axios({
                    method: 'post',
                    url: permissionUrl.getPage,
                    data: searchModel
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
            //获取多个permissionId字符串
            getIds() {
                var items = this.multipleSelection;
                var ids = [];
                items.map(function (item) { ids.push(item.permissionId); })
                return ids;
            },
            //#endregion

            //#region 分页
            //val页大小(每页多少条)
            handleSizeChange(val) {
                this.pagingModel.pageSize = val;
            },
            //val页索引(当前第几页)
            handleCurrentChange(val) {
                this.pagingModel.currentPage = val;
            },
            //#endregion

            //保存
            sava(formName) {
                var ids = this.entityModel.ids;
                var parentId = ids[ids.length - 1];
                if (parentId !== this.entityModel.permissionId) {
                    this.entityModel.parentId = parentId;
                }
                var _this = this;
                var apiUrl = '';
                var requestMethod = '';
                if (this.isEdit) {
                    //修改
                    apiUrl = permissionUrl.modInfo;
                    requestMethod = 'put';
                } else {
                    //添加
                    apiUrl = permissionUrl.addInfo;
                    requestMethod = 'post';
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
            //多选（表格全选，反选）
            handleSelectionChange(val) {
                this.multipleSelection = val;
            },
            //根据ID获取对象数据
            getObjectDataById(row) {
                var permissionId = row.permissionId;
                this.searchModel.parentId = permissionId;
                this.getPage();
                this.getPath();
            },
            //面包屑链接对象数据
            onLinkObjectData(permissionId) {
                this.searchModel.parentId = permissionId;
                this.searchModel.permissionName = "";
                this.getPage();
                this.getPath();
            },
            //对树节点进行筛选时执行的方法，返回 true 表示这个节点可以显示，返回 false 则表示这个节点会被隐藏
            filterNode(value, data) {
                if (!value) return true;
                return data.permissionName.indexOf(value) !== -1;
            },
            //点击树节点执行
            handleNodeClick(data) {
                this.onLinkObjectData(data.permissionId);
            },
            handleItemChange() { }
        },
        watch: {
            //分页数据监测绑定列表
            pagingModel: {
                deep: true,
                handler: function () {
                    this.getPage();
                }
            },
            //监听搜索文本过滤树控件数据
            filterText(val) {
                this.$refs.tree.filter(val);
            },
            //监听添加修改弹出窗口是否打开关闭
            addOrModDialogFormVisible() {
                //if (val) {

                //}
                if (this.searchModel.clientCategorys.length > 0) {
                    this.entityModel.clientCategorys = this.searchModel.clientCategorys;
                    this.entityModel.clientCategory = this.searchModel.clientCategorys[0].value;
                }
                if (this.searchModel.dataStates.length > 0) {
                    this.entityModel.dataStates = this.searchModel.dataStates;
                    this.entityModel.dataState = this.searchModel.dataStates[0].value;
                }
            }
        }
    }
</script>

<style scoped>
    .el-header {
        height: 100% !important;
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