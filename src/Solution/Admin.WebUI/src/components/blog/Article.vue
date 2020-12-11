<template>
    <!--外容器-->
    <el-container>
        <!--上面层-->
        <el-header class="search-header">
            <!--搜索栏-->
            <el-card class="search-card">
                <el-form :inline="true" :model="searchModel" class="search-form-inline">
                    <el-form-item label="标题">
                        <el-input v-model="searchModel.title" placeholder="" clearable></el-input>
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
                    <!--按钮组-->
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
                        <!--<el-table-column prop="title" label="详细" width="120" fixed type="expand">
                            <template slot-scope="props">
                                <el-form label-position="left" inline class="demo-table-expand">
                                    <el-form-item label="身份证号">
                                        <span>{{ props.row.identityCard }}</span>
                                    </el-form-item>
                                    <el-form-item label="移动电话">
                                        <span>{{ props.row.mobilePhone }}</span>
                                    </el-form-item>
                                    <el-form-item label="电子邮箱">
                                        <span>{{ props.row.email }}</span>
                                    </el-form-item>
                                    <el-form-item label="生日">
                                        <span>{{ props.row.birthday }}</span>
                                    </el-form-item>
                                    <el-form-item label="地址">
                                        <span>{{ props.row.address }}</span>
                                    </el-form-item>
                                </el-form>
                            </template>
                        </el-table-column>-->
                        <el-table-column prop="id" label="ID" width="120" fixed sortable></el-table-column>
                        <el-table-column prop="title" label="标题" fixed width="120">
                            <template slot-scope="scope">
                                <el-popover trigger="hover" placement="top">
                                    <p>修改时间: {{ scope.row.modifyTime }}</p>
                                    <p>创建时间: {{ scope.row.createTime }}</p>
                                    <div slot="reference" class="name-wrapper">
                                        <el-tag size="medium">{{ scope.row.title }}</el-tag>
                                    </div>
                                </el-popover>
                            </template>
                        </el-table-column>
                        <el-table-column prop="content" label="内容" width="120" fixed show-overflow-tooltip></el-table-column>
                        <el-table-column prop="sort" label="排序"></el-table-column>
                        <el-table-column prop="remark" label="备注"></el-table-column>
                        <el-table-column prop="modifyTime" label="修改时间" width="160"></el-table-column>
                        <el-table-column prop="createTime" label="创建时间" width="160"></el-table-column>
                        <el-table-column prop="modifierId" label="修改者ID"></el-table-column>
                        <el-table-column prop="creatorId" label="创建者ID"></el-table-column>
                        <el-table-column prop="dataState" label="数据状态" fixed="right">
                            <template slot-scope="scope">
                                <span v-html="scope.row.dataState"></span>
                            </template>
                        </el-table-column>
                        <!--<el-table-column fixed="right" label="操作" width="120">
                            <template slot-scope="scope">
                                <el-button @click.native.prevent="onSee(scope.row)" type="text" size="small">查看</el-button>
                                <el-button @click.native.prevent="mod(scope.row)" type="text" size="small">编辑</el-button>
                                <el-button @click.native.prevent="del(scope.row)" type="text" size="small">删除</el-button>
                            </template>
                        </el-table-column>-->
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
        <el-dialog title="用户信息" :visible.sync="addOrModDialogVisible">
            <el-form :inline="true"
                     :model="entityModel"
                     :rules="rules"
                     ref="editForm"
                     label-width="80px"
                     :label-position="labelPosition">
                <el-form-item label="标题" prop="title">
                    <el-input placeholder="" v-model="entityModel.title" autocomplete="off" clearable></el-input>
                </el-form-item>
                <el-form-item label="内容" prop="content">
                    <el-input placeholder="" v-model="entityModel.content" autocomplete="off" clearable></el-input>
                </el-form-item>
                <br />
                <el-form-item label="文章类型" prop="articleType">
                    <el-select v-model="entityModel.articleType" placeholder="请选择">
                        <el-option v-for="item in articleTypes"
                                   :key="item.value"
                                   :label="item.label"
                                   :value="item.value">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="排序" prop="sort">
                    <el-input placeholder="" v-model="entityModel.sort" autocomplete="off" clearable></el-input>
                </el-form-item>
                <br />
                <el-form-item label="备注" prop="remark">
                    <el-input placeholder="" v-model="entityModel.remark" autocomplete="off" clearable></el-input>
                </el-form-item>
                <br />
                <el-form-item label="数据状态" prop="dataState">
                    <el-select v-model="entityModel.dataState" placeholder="请选择">
                        <el-option v-for="item in dataStates"
                                   :key="item.value"
                                   :label="item.label"
                                   :value="item.value">
                        </el-option>
                    </el-select>
                </el-form-item>
            </el-form>
            <div slot="footer" class="dialog-footer">
                <el-button @click="addOrModDialogVisible = false">取 消</el-button>
                <el-button @click="resetForm('editForm')">重置</el-button>
                <el-button type="primary" @click="save('editForm')">确 定</el-button>
            </div>
        </el-dialog>
    </el-container>
</template>

<script>
    import { timeFormat } from "@/js/common/utility.js";
    import { paginationModel as pagingModel } from '@/js/common/pagination';
    import { enumUrl, articleUrl } from "@/js/common/api.js";
    export default {
        name: 'ArticleTemplate',
        data() {
            return {
                pagingModel,
                searchModel: {
                    title: ''
                },
                entityModel: {
                    id: 0,
                    title: '',
                    content: '',
                    articleType: 0,
                    remark: '',
                    sort: 0,
                    dataState: 1
                },
                rules: {
                    title: [
                        { required: true, message: '请输入标题', trigger: 'blur' }
                    ],
                    content: [
                        { required: true, message: '请输入内容', trigger: 'blur' }
                    ]
                },
                datas: [],
                dataStates: [],
                articleTypes: [],
                addOrModDialogVisible: false,
                multipleSelection: [],
                labelPosition: 'right'
            }
        },
        components: {

        },
        created: function () {
            this.getPage();
            //this.getGetDataState();
            //this.getArticleType();
        },
        mounted: function () {

        },
        computed: {

        },
        methods: {
            //添加
            add() {
                this.entityModel.id = 0;
                this.entityModel.title = '';
                this.entityModel.content = '';
                this.entityModel.articleType = 0;
                this.entityModel.remark = '';
                this.entityModel.sort = 0;
                this.entityModel.dataState = 1;
                this.addOrModDialogVisible = true;
            },
            //编辑
            mod() {
                var _this = this;
                var ids = _this.getIds();
                if (ids.length <= 0 || ids.length > 1) {
                    this.$message({
                        type: 'info',
                        message: '请选择一条数据修改'
                    });
                    return;
                }
                _this.$axios({
                    method: 'get',
                    url: articleUrl.getInfo,
                    params: { id: ids[0] }
                }).then(function (response) {
                    _this.entityModel = response.data.data;
                    _this.addOrModDialogVisible = true;
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
                        url: articleUrl.delInfo,
                        data: ids
                    }).then(function (response) {
                        if (response.data.resultState > 0) {
                            _this.$message({
                                type: 'success',
                                message: '删除成功!'
                            });
                            //刷新列表
                            _this.getPage();
                        } else {
                            _this.$message({
                                type: 'error',
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
                    apiUrl = articleUrl.addInfo;
                    requestMethod = 'post';
                } else {
                    //修改
                    apiUrl = articleUrl.modInfo;
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
                                        _this.addOrModDialogVisible = false;
                                        //刷新数据列表
                                        _this.getPage();
                                    }
                                });
                            } else {
                                _this.$message({
                                    type: 'info',
                                    message: '提示，不能添加相同名称的数据'
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
                    title: this.searchModel.title
                };
                _this.$axios({
                    method: 'post',
                    url: articleUrl.getPage,
                    data: searchData
                }).then(function (response) {
                    var retDatas = response.data;
                    if (retDatas.datas === null) {
                        _this.$message({
                            type: 'info',
                            message: '提示，无此记录信息'
                        });
                        return;
                    }
                    retDatas.datas.forEach(obj => {
                        obj.createTime !== null ? obj.createTime = timeFormat(obj.createTime) : obj.createTime = null;
                        obj.modifyTime !== null ? obj.modifyTime = timeFormat(obj.modifyTime) : obj.modifyTime = null;
                        switch (obj.articleType) {
                            case 1:
                                obj.articleType = '随心日记'
                                break;
                            case 2:
                                obj.articleType = '技术文章'
                                break;
                            default:
                        }
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
                    _this.datas = retDatas.datas;
                    _this.pagingModel.total = retDatas.total;
                });
            },
            getGetDataState() {
                var _this = this;
                _this.$axios({
                    method: 'get',
                    url: enumUrl.getDataState
                }).then(function (response) {
                    _this.dataStates = response.data;
                });
            },
            getArticleType() {
                var _this = this;
                _this.$axios({
                    method: 'get',
                    url: enumUrl.GetArticleType
                }).then(function (response) {
                    _this.articleTypes = response.data;
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
        height: 100% !important;
    }

    .search-form-inline {
        margin-top: 20px;
    }

    .search-card, .operation-card, .pagination-card {
        text-align: left;
    }
</style>