<template>
    <!--外容器-->
    <el-container>
        <!--上面层-->
        <el-header class="search-header">
            <!--搜索栏-->
            <el-card class="search-card">
                <el-form :inline="true" :model="searchModel" class="search-form-inline">
                    <el-form-item label="名称">
                        <el-input v-model="searchModel.companyName" placeholder="名称" clearable></el-input>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" icon="el-icon-search" @click="search">查询</el-button>
                    </el-form-item>
                </el-form>
            </el-card>
        </el-header>
        <!--下面层-->
        <el-container>
            <el-header>
                <!--操作栏-->
                <el-card class="operation-card">
                    <el-button-group>
                        <el-button type="primary" icon="el-icon-circle-plus-outline" @click="add">添加</el-button>
                        <el-button type="primary" icon="el-icon-edit" @click="mod">修改</el-button>
                        <el-button type="primary" icon="el-icon-delete" @click="del">删除</el-button>
                    </el-button-group>
                </el-card>
            </el-header>
            <el-main>
                <!--表格栏-->
                <el-card class="table-card">
                    <el-table :data="datas"
                              style="width: 100%"
                              max-height="455"
                              stripe :default-sort="{prop: 'id', order: 'descending'}"
                              @selection-change="handleSelectionChange">
                        <el-table-column fixed type="selection" width="55"></el-table-column>
                        <el-table-column fixed prop="id" label="ID" width="120" sortable></el-table-column>
                        <el-table-column fixed prop="companyName" label="名称" width="260"></el-table-column>
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
        <el-dialog title="公司信息" :visible.sync="dialogFormVisible">
            <el-form :inline="true" :model="entityModel" :rules="rules" ref="editForm">
                <el-form-item label="公司名称" prop="companyName" style="margin-bottom:15px;">
                    <el-input placeholder="请输入名称" v-model="entityModel.companyName" autocomplete="off" clearable></el-input>
                </el-form-item>
                <el-form-item label="公司排序" prop="sort">
                    <el-input-number placeholder="排序" v-model="entityModel.sort" :min="0" :max="10" label="公司排序"></el-input-number>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="dialogFormVisible = false">取 消</el-button>
                <el-button @click="resetForm('editForm')">重置</el-button>
                <el-button type="primary" @click="save('editForm')">确 定</el-button>
            </div>
        </el-dialog>
    </el-container>
</template>

<script>
    import { timeFormat } from "@/js/common/utility.js";
    import { paginationModel as pagingModel } from '@/js/common/pagination';
    import { companyUrl } from "@/js/common/api.js";
    export default {
        name: 'CompanyTemplate',
        data() {
            return {
                pagingModel,
                searchModel: {
                    companyName: ''
                },
                entityModel: {
                    id: 0,
                    companyName: '',
                    sort: 0
                },
                rules: {
                    companyName: [
                        { required: true, message: '请输入名称', trigger: 'blur' }
                    ]
                },
                datas: [],
                dialogFormVisible: false
            }
        },
        components: {

        },
        created: function () {
            this.getPage();
        },
        mounted: function () {

        },
        computed: {

        },
        methods: {
            //添加
            add() {
                this.entityModel.id = 0;
                this.entityModel.companyName = '';
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
                _this.$axios({
                    method: 'get',
                    url: companyUrl.getInfo,
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
                if (ids.length <= 0) {
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
                        url: companyUrl.delInfo,
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
                                type: 'success',
                                message: '删除失败!'
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
            save(formName) {
                var _this = this;
                var apiUrl = '';
                var requestMethod = '';
                if (this.entityModel.id === 0) {
                    //添加
                    apiUrl = companyUrl.addInfo;
                    requestMethod = 'post';
                } else {
                    //修改
                    apiUrl = companyUrl.modInfo;
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
                                        //_this.dialogFormVisible = false;
                                        _this.entityModel.companyName = '';
                                        _this.getPage();
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
            //绑定表格数据（获取用户列表）
            getPage() {
                var _this = this;
                var searchData = {
                    pageIndex: this.pagingModel.currentPage,
                    pageSize: this.pagingModel.pageSize,
                    companyName: this.searchModel.companyName
                };
                _this.$axios({
                    method: 'post',
                    url: companyUrl.getPage,
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
            //获取多个id字符串
            getIds() {
                var items = this.multipleSelection;
                var ids = [];
                items.map(function (item) { ids.push(item.id); })
                return ids;
            }
        },
        watch: {
            pagingModel: {
                deep: true,
                handler: function () {
                    this.getPage();
                }
            }
        }
    }
</script>

<style scoped>
    .search-header {
        height: 124px !important;
    }

    .search-form-inline {
        margin-top: 20px;
    }

    .search-card, .operation-card, .pagination-card {
        text-align: left;
    }
</style>