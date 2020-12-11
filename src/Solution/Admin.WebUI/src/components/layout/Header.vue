<template>
    <el-row>
        <el-col :span="22">
            <el-button class="fold-button" type="text" :icon="bindIcon" @click="collapseChage"></el-button>
        </el-col>
        <el-col :span="2" class="col2">
            <el-dropdown>
                <el-button type="text">欢迎您 {{loginInfo.userName}}<i class="el-icon-arrow-down el-icon--right"></i></el-button>
                <el-dropdown-menu slot="dropdown">
                    <el-dropdown-item><a href="https://www.baidu.com/" target="_blank">百度搜索</a></el-dropdown-item>
                    <el-dropdown-item><a href="https://cn.vuejs.org/" target="_blank">Vue官网</a></el-dropdown-item>
                    <el-dropdown-item><a href="https://element.eleme.cn/#/zh-CN" target="_blank">Element</a></el-dropdown-item>
                    <el-dropdown-item divided><a href="Login">退出登录</a></el-dropdown-item>
                </el-dropdown-menu>
            </el-dropdown>
        </el-col>
    </el-row>
</template>

<script>
    export default {
        name: 'HeaderTemplate',
        data() {
            return {
                isCollapse: false,
                bindIcon: "el-icon-s-fold",
                loginInfo: {
                    userName: ""
                }
            }
        },
        components: {

        },
        created() {
            this.$store.commit("getLoginInfoMutations");
            this.loginInfo.userName = this.$store.state.loginInfo.user.userName;
        },
        mounted() {

        },
        methods: {
            collapseChage() {
                this.isCollapse = !this.isCollapse;
                this.$eventBus.$emit('isCollapse', this.isCollapse);
            }
        },
        watch: {
            isCollapse(newName, oldName) {
                this.bindIcon = newName == true ? "el-icon-s-unfold" : "el-icon-s-fold";
            }
        }
    };
</script>

<style scoped>
    .col2 {
        text-align: center;
    }

    .fold-button {
        font-size: 20px;
    }
</style>