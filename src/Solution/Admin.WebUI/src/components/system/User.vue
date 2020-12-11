<template>
    <!--外容器-->
    <el-container>
        <!--上面层-->
        <el-header class="search-header">
            <!--搜索栏-->
            <el-card class="search-card">
                <el-form :inline="true" :model="searchModel" class="search-form-inline">
                    <el-form-item label="姓名">
                        <el-input v-model="searchModel.userName" placeholder="姓名" clearable></el-input>
                    </el-form-item>
                    <el-form-item label="生日">
                        <el-date-picker type="date" placeholder="选择日期" format="yyyy 年 MM 月 dd 日" value-format="yyyy-MM-dd" v-model="searchModel.birthday">
                        </el-date-picker>
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
                        <el-table-column prop="userName" label="详细" width="120" fixed type="expand">
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
                        </el-table-column>
                        <el-table-column prop="id" label="ID" width="120" fixed sortable></el-table-column>
                        <el-table-column prop="userName" label="姓名" fixed width="120">
                            <template slot-scope="scope">
                                <el-popover trigger="hover" placement="top">
                                    <p>用户昵称: {{ scope.row.nickName }}</p>
                                    <p>真实姓名: {{ scope.row.realName }}</p>
                                    <div slot="reference" class="name-wrapper">
                                        <el-tag size="medium">{{ scope.row.userName }}</el-tag>
                                    </div>
                                </el-popover>
                            </template>
                        </el-table-column>
                        <el-table-column prop="password" label="密码" width="120" fixed show-overflow-tooltip></el-table-column>
                        <el-table-column prop="sex" label="性别" width=""></el-table-column>
                        <el-table-column prop="birthday" label="生日" show-overflow-tooltip></el-table-column>
                        <el-table-column prop="companyUrl.companyName" label="所属公司" show-overflow-tooltip></el-table-column>
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
                <el-form-item label="用户名称" prop="userName">
                    <el-input placeholder="请输入名称" v-model="entityModel.userName" autocomplete="off" clearable></el-input>
                </el-form-item>
                <el-form-item label="用户密码" prop="password">
                    <el-input type="password" placeholder="请输入密码" v-model="entityModel.password" clearable show-password></el-input>
                </el-form-item>
                <br />
                <el-form-item label="用户生日" prop="birthday">
                    <el-date-picker type="date" placeholder="选择日期" format="yyyy 年 MM 月 dd 日" value-format="yyyy-MM-dd" v-model="entityModel.birthday">
                    </el-date-picker>
                </el-form-item>
                <el-form-item label="真实姓名" prop="realName">
                    <el-input placeholder="" v-model="entityModel.realName" autocomplete="off" clearable></el-input>
                </el-form-item>
                <br />
                <el-form-item label="用户性别" prop="sex">
                    <el-radio-group v-model="entityModel.sex" style="width:217px; text-align:left;">
                        <el-radio v-for="item in sexs" :key="item.value" :label="item.label"></el-radio>
                    </el-radio-group>
                </el-form-item>
                <el-form-item label="手机号码" prop="mobilePhone">
                    <el-input placeholder="" v-model="entityModel.mobilePhone" autocomplete="off" clearable></el-input>
                </el-form-item>
                <br />
                <el-form-item label="所属部门" prop="departmentId">
                    <el-select v-model="entityModel.departmentId" placeholder="请选择">
                        <el-option v-for="item in departments"
                                   :key="item.id"
                                   :label="item.departmentName"
                                   :value="item.id">
                        </el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="用户角色" prop="roleId">
                    <el-select v-model="entityModel.roleIds" placeholder="请选择" multiple collapse-tags>
                        <el-option v-for="item in roles"
                                   :key="item.id"
                                   :label="item.roleName"
                                   :value="item.id">
                        </el-option>
                    </el-select>
                </el-form-item>
                <br />
                <el-form-item label="电子邮箱" prop="email">
                    <el-input v-model="entityModel.email" clearable></el-input>
                </el-form-item>
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
    import { enumUrl, userUrl, departmentUrl, roleUrl } from "@/js/common/api.js";
    export default {
        name: 'UserTemplate',
        data() {
            return {
                pagingModel,
                searchModel: {
                    userName: ''
                },
                entityModel: {
                    id: 0,
                    userName: '',
                    password: '',
                    departmentId: 0,
                    sex: 2,
                    email: '',
                    address: '',
                    birthday: '',
                    realName: '',
                    mobilePhone: '',
                    remark: '',
                    sort: 0,
                    dataState: 1,
                    roleIds: []
                },
                rules: {
                    userName: [
                        { required: true, message: '请输入名称', trigger: 'blur' },
                        { min: 1, max: 12, message: '长度在 3 到 12 个字符', trigger: 'blur' }
                    ],
                    password: [
                        { required: true, message: '请输入密码', trigger: 'blur' }
                    ],
                    birthday: [
                        { required: true, message: '请输入生日', trigger: 'blur' }
                    ]
                },
                datas: [],
                departments: [],
                roles: [],
                dataStates: [],
                sexs: [],
                addOrModDialogVisible: false,
                multipleSelection: [],
                labelPosition: 'right'
            }
        },
        components: {

        },
        created: function () {
            this.getPage();
            this.getDepartmentList();
            this.getRoleList();
            this.getGetDataState();
            this.getSexType();
        },
        mounted: function () {

        },
        computed: {

        },
        methods: {
            //添加
            add() {
                this.entityModel.id = 0;
                this.entityModel.userName = '';
                this.entityModel.password = '';
                this.entityModel.departmentId = null;
                this.entityModel.sex = 0;
                this.entityModel.email = '';
                this.entityModel.address = '';
                this.entityModel.birthday = '';
                this.entityModel.realName = '';
                this.entityModel.mobilePhone = '';
                this.entityModel.remark = '';
                this.entityModel.sort = 0;
                this.entityModel.dataState = 1;
                this.entityModel.roleIds = [];
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
                    url: userUrl.getInfo,
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
                        url: userUrl.delInfo,
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
                    apiUrl = userUrl.addInfo;
                    requestMethod = 'post';
                } else {
                    //修改
                    apiUrl = userUrl.modInfo;
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
                    userName: this.searchModel.userName,
                    birthday: this.searchModel.birthday
                };
                _this.$axios({
                    method: 'post',
                    url: userUrl.getPage,
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
                        obj.birthday !== null ? obj.birthday = timeFormat(obj.birthday) : obj.birthday = null;
                        switch (obj.sex) {
                            case 0:
                                obj.sex = '男'
                                break;
                            case 1:
                                obj.sex = '女'
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
            getDepartmentList() {
                var _this = this;
                _this.$axios({
                    method: 'get',
                    url: departmentUrl.getList
                }).then(function (response) {
                    var retDatas = response.data;
                    _this.departments = retDatas.datas;
                });
            },
            getRoleList() {
                var _this = this;
                _this.$axios({
                    method: 'get',
                    url: roleUrl.getList
                }).then(function (response) {
                    var retDatas = response.data;
                    _this.roles = retDatas.datas;
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
            getSexType() {
                var _this = this;
                _this.$axios({
                    method: 'get',
                    url: enumUrl.getSexType
                }).then(function (response) {
                    _this.sexs = response.data;
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