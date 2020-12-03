<template>
    <!--外容器-->
    <el-container>
        <!--上面层-->
        <el-header>
            <!--搜索栏-->
            <el-card class="search-card">
                <el-form :inline="true" :model="searchModel" class="search-form-inline">
                    <el-form-item label="关键字">
                        <el-input v-model="searchModel.menuName" placeholder="名称" clearable></el-input>
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
                        <el-breadcrumb-item v-for="(o,index) in paths" :key="index" @click.native.prevent="onLinkObjectData(o.id)">
                            <a href="#">{{o.menuName}}</a>
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
                              stripe :default-sort="{prop: 'id', order: 'descending'}"
                              @selection-change="handleSelectionChange">
                        <el-table-column fixed type="selection" width="55"></el-table-column>
                        <el-table-column prop="id" label="菜单编号" width="120" sortable></el-table-column>
                        <el-table-column prop="menuName" label="菜单名称" show-overflow-tooltip width="">
                            <template slot-scope="scope">
                                <el-button type="text" size="small" @click="getListById(scope.row)">{{ scope.row.menuName }}</el-button>
                            </template>
                        </el-table-column>
                        <el-table-column prop="terminalCategory" label="终端类别" width="120" sortable></el-table-column>
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
        <el-dialog title="菜单信息" :visible.sync="dialogFormVisible">
            <el-form :inline="false"
                     :model="entityModel"
                     :rules="rules"
                     ref="editForm"
                     label-width="auto">
                <el-row :gutter="0" type="flex" justify="center">
                    <el-col :span="24">
                        <el-form-item label="菜单名称" prop="menuName">
                            <el-input style="width:200px; float:left;" placeholder="名称" v-model="entityModel.menuName" autocomplete="off" clearable></el-input>
                        </el-form-item>
                        <el-form-item label="所属菜单">
                            <el-cascader style="width:200px; float:left;" :options="nodes"
                                         v-model="entityModel.ids"
                                         change-on-select
                                         filterable
                                         :clearable="true"
                                         :props="nodeProps"
                                         @change="handleItemChange"
                                         expand-trigger="hover">
                            </el-cascader>
                        </el-form-item>
                        <el-form-item label="客户端类别">
                            <el-select style="width:200px; float:left;" v-model="entityModel.clientCategory" placeholder="请选择">
                                <el-option v-for="item in entityModel.clientCategorys"
                                           :key="item.value"
                                           :label="item.label"
                                           :value="item.value">
                                </el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="菜单状态">
                            <el-select style="width:200px; float:left;" v-model="entityModel.dataState" placeholder="请选择">
                                <el-option v-for="item in entityModel.dataStates"
                                           :key="item.value"
                                           :label="item.label"
                                           :value="item.value">
                                </el-option>
                            </el-select>
                        </el-form-item>
                        <el-form-item label="菜单排序" prop="sort">
                            <el-input-number style="width:200px; float:left;" placeholder="排序" v-model="entityModel.sort" :min="0" :max="10" label="排序"></el-input-number>
                        </el-form-item>
                        <el-form-item label="备注">
                            <el-input style="width:200px; float:left;" type="textarea"
                                      :rows="2"
                                      placeholder="请输入内容"
                                      v-model="entityModel.remark">
                            </el-input>
                        </el-form-item>
                    </el-col>
                </el-row>

            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogFormVisible = false">取 消</el-button>
                <el-button @click="resetForm('editForm')">重置</el-button>
                <el-button type="primary" @click="save('editForm')">保存并添加</el-button>
                <el-button type="primary" @click="savec('editForm')">保存并关闭</el-button>
            </div>
        </el-dialog>
    </el-container>
</template>

<script>
    import { timeFormat } from "@/js/common/utility.js";
    import { paginationModel as pagingModel } from '@/js/common/pagination';
    import { enumUrl, menuUrl } from "@/js/common/api.js";
    export default {
        name: 'MenuTemplate',
        data() {
            return {
                pagingModel,
                searchModel: {
                    parentId: null,
                    menuName: ''
                },
                entityModel: {
                    id: 0,
                    parentId: null,
                    ids: [],
                    menuName: '',
                    sort: 0
                },
                rules: {
                    menuName: [
                        { required: true, message: '请输入名称', trigger: 'blur' }
                    ]
                },
                treeProps: {
                    id: 'id',
                    label: 'menuName',
                    children: 'children'
                },
                nodeProps: {
                    value: 'id',
                    label: 'menuName',
                    children: 'children',
                    disabled: 'disabled',
                    expandTrigger: 'hover',
                    checkStrictly: 'false'
                    //lazy: true,//开启动态加载(懒加载)
                    //lazyLoad: (node, resolve) => {
                    //    var nodeData = node.data;
                    //    if (nodeData !== undefined) {
                    //        var _this = this;
                    //        var pid = node.data.id;
                    //        const apiUrl = 'https://localhost:44372/api/Menus/BindingData';
                    //        _this.$axios({
                    //            method: 'post',
                    //            url: apiUrl,
                    //            params: { parentId: pid }
                    //        }).then(function (response) {
                    //            var retDatas = response.data;
                    //            resolve(retDatas);
                    //        });
                    //    }
                    //}
                },
                nodes: [],
                trees: [],
                datas: [],
                paths: [],
                multipleSelection: [],
                inputIsDisabled: false,
                dialogFormVisible: false,
                //筛选文本，过滤树控件的检索文本
                filterText: '',
                //Select选择器
                selectlOptions: [],
                //select默认选中值
                value: ''
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
            this.getDataState();
            this.getClientCategory();
        },
        computed: {
        },
        methods: {
            //添加
            add() {
                this.entityModel.id = 0;
                this.entityModel.parentId = null;
                this.entityModel.ids = [];
                this.entityModel.menuName = '';
                this.entityModel.sort = 0;
                this.dialogFormVisible = true;
            },
            //编辑
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
                    url: menuUrl.getInfo,
                    params: { id: ids[0] }
                }).then(function (response) {
                    _this.entityModel = response.data;
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
                        url: menuUrl.delInfo,
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
            savec(formName) {
                this.save(formName);
                this.dialogFormVisible = false;
            },
            //保存
            save(formName) {
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
                    apiUrl = menuUrl.addInfo;
                    requestMethod = 'post';
                } else {
                    //修改
                    apiUrl = menuUrl.modInfo;
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
            //绑定表格数据（获取数据列表）
            getPage() {
                var _this = this;
                var searchData = {
                    pageIndex: this.pagingModel.currentPage,
                    pageSize: this.pagingModel.pageSize,
                    parentId: this.searchModel.parentId,
                    menuName: this.searchModel.menuName
                };
                _this.$axios({
                    method: 'post',
                    url: menuUrl.getPage,
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
                    url: menuUrl.getNode,
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
                    url: menuUrl.getTree
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
                    url: menuUrl.getPath,
                    params: { id: _this.searchModel.parentId }
                }).then(function (response) {
                    _this.paths = response.data;
                });
            },
            //cascader选中改变事件
            handleItemChange(val) {
                if (val.length > 1) {
                    this.isDisabled = true;
                } else {
                    this.isDisabled = false;
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
            //根据父ID获取对象数据列表
            getObjectDataById(row) {
                var id = row.id;
                this.searchModel.id = id;
                this.getPage();
                this.getPath();
            },
            //链接对象数据
            onLinkObjectData(parentId) {
                if (parentId === 0) { parentId = null; }
                this.searchModel.parentId = parentId;
                this.searchModel.menuName = "";
                this.getPage();
                this.getPath();
            },
            //对树节点进行筛选时执行的方法，返回 true 表示这个节点可以显示，返回 false 则表示这个节点会被隐藏
            filterNode(value, data) {
                if (!value) return true;
                return data.menuName.indexOf(value) !== -1;
            },
            //点击树节点执行
            handleNodeClick(data) {
                this.onLinkObjectData(data.id);
            },
            //获取数据状态
            getDataState() {
                var _this = this;
                this.$axios({
                    method: 'get',
                    url: enumUrl.getDataState
                }).then(function (response) {
                    var retDatas = response.data;
                    _this.searchModel.dataStates = retDatas;
                    _this.entityModel.dataStates = retDatas;
                    _this.searchModel.dataState = retDatas[0].value;
                    _this.entityModel.dataState = retDatas[0].value;
                });
            },
            //获取系统终端类型
            getClientCategory() {
                var _this = this;
                this.$axios({
                    method: 'get',
                    url: enumUrl.getClientCategory
                }).then(function (response) {
                    var retDatas = response.data;
                    _this.searchModel.clientCategorys = retDatas;
                    _this.entityModel.clientCategorys = retDatas;
                    _this.searchModel.clientCategory = retDatas[0].value;
                    _this.entityModel.clientCategory = retDatas[0].value;
                });
            }
        },
        watch: {
            //分页数据监测绑定列表
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