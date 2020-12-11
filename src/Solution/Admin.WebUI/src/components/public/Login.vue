<template>
    <div class="login">
        <h2>后台管理系统</h2>
        <el-form ref="loginForm" :model="loginForm" status-icon :rules="rules" label-width="0px">
            <el-form-item label="" prop="userName">
                <el-input v-model="loginForm.userName" placeholder="用户名" prefix-icon="element-icons el-icon-userName" clearable></el-input>
            </el-form-item>
            <el-form-item label="" prop="passWord">
                <el-input type="password" v-model="loginForm.passWord" placeholder="密   码" prefix-icon="element-icons el-icon-passWord" clearable></el-input>
            </el-form-item>
            <el-form-item label="" prop="veriCode" style="height:40px;">
                <el-col :span="12">
                    <el-input v-model="loginForm.veriCode" clearable placeholder="验证码" prefix-icon="element-icons el-icon-veriCode"></el-input>
                </el-col>
                <el-col :span="12">
                    <img id="nubImg" title="登录验证码" v-bind:src="imgUrl" alt="vcode" v-on:click="refresh" style="cursor:pointer;" />
                </el-col>
            </el-form-item>
            <el-form-item label="">
                <el-col :span="12">
                    <el-checkbox label="记住密码(记住七天时间)" name="type" v-model="remember"></el-checkbox>
                </el-col>
                <el-col :span="6">
                    <el-button plain type="primary" @click="submitForm('loginForm')" style="width:100px;">登录</el-button>
                </el-col>
                <el-col :span="6">
                    <el-button plain type="info" @click="resetForm('loginForm')" style="width:100px;">重置</el-button>
                </el-col>
            </el-form-item>
        </el-form>
    </div>
</template>

<script>
    import { loginUrl } from "@/js/common/api.js";
    export default {
        name: 'LoginTemplate',
        data() {
            return {
                loginForm: {
                    userName: '',
                    passWord: '',
                    veriCode: 'wjk'
                },
                remember: true,
                imgUrl: loginUrl.verificationCode,
                rules: {
                    userName: [
                        { required: true, message: '请输入用户名称', trigger: 'blur' }
                    ],
                    passWord: [
                        { required: true, message: '请输入用户密码', trigger: 'blur' }
                    ],
                    veriCode: [
                        { required: true, message: '请输入验证码', trigger: 'blur' }
                    ]
                }
            }
        },
        created: function () {

        },
        mounted: function () {
            //页面加载调用获取cookie值
            this.getCookie();
        },
        methods: {
            refresh() {
                this.imgUrl = loginUrl.verificationCode + '?' + Math.random();
            },
            submitForm(formName) {
                var _this = this;
                this.$refs[formName].validate(function (valid) {
                    if (valid) {
                        if (_this.remember == true) {
                            //传入账号名，密码，和保存天数3个参数
                            _this.setCookie(_this.loginForm.userName, _this.loginForm.passWord, 7);
                        } else {
                            //清空Cookie
                            _this.clearCookie();
                        }
                        _this.$axios({
                            method: 'post',
                            url: loginUrl.verificationLogin,
                            data: {
                                userName: _this.loginForm.userName,
                                passWord: _this.loginForm.passWord,
                                veriCode: _this.loginForm.veriCode
                            }
                        }).then(function (response) {
                            if (response.data === -1) {
                                _this.$message('验证码不正确!');
                                return;
                            }
                            if (response.data === 0) {
                                _this.$message.error('账号或密码不正确或用户不存在或用户未启用!');
                                return;
                            }

                            if (!response.data.user) {
                                _this.$message.error(response.data);
                                return;
                            }
                            _this.$message({
                                message: '登录成功!',
                                type: 'success'
                            });
                            //登录信息存入VuexStore
                            var loginInfo = { user: response.data.user, token: response.data.token };
                            _this.$store.commit('setLoginInfoMutations', loginInfo);
                            //登录成功跳转到主页面
                            _this.$router.push({ path: '/Home' });
                        });
                    } else {
                        return false;
                    }
                });
            },
            resetForm(formName) {
                this.$refs[formName].resetFields();
            },
            //设置cookie
            setCookie(c_name, c_pwd, exdays) {
                //获取时间
                var exdate = new Date();
                //保存的天数
                exdate.setTime(exdate.getTime() + 24 * 60 * 60 * 1000 * exdays);
                //字符串拼接cookie
                window.document.cookie = "userName" + "=" + c_name + ";path=/;expires=" + exdate.toGMTString();
                window.document.cookie = "userPwd" + "=" + c_pwd + ";path=/;expires=" + exdate.toGMTString();
            },
            //读取cookie
            getCookie: function () {
                if (document.cookie.length > 0) {
                    //这里显示的格式需要切割一下自己可输出看下
                    var arr = document.cookie.split('; ');
                    for (var i = 0; i < arr.length; i++) {
                        //再次切割
                        var arr2 = arr[i].split('=');
                        //判断查找相对应的值
                        if (arr2[0] == 'userName') {
                            //保存到保存数据的地方
                            this.loginForm.userName = arr2[1];
                        } else if (arr2[0] == 'userPwd') {
                            this.loginForm.passWord = arr2[1];
                        }
                    }
                }
            },
            //清除cookie
            clearCookie: function () {
                //修改2值都为空，天数为负1天就好了
                this.setCookie("", "", -1);
            }
        },
        watch: {}
    }
</script>

<style scoped>
    h2 {
        text-align: center;
        margin: 15px auto;
        font-weight: 300;
        font-size: 30px;
        color: #000;
    }

    .login {
        padding-left: 30px;
        padding-right: 30px;
        width: 450px;
        height: 320px;
        margin: auto;
        position: absolute;
        top: 0;
        left: 0;
        bottom: 0;
        right: 0;
        border: 0px solid burlywood;
        box-shadow: darkgrey 10px 10px 30px 5px;
    }
</style>