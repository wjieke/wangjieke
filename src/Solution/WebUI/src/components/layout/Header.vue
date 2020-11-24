<template>
    <el-row>
        <el-col :span="22">
            <el-button type="info" class="btn-menu" :icon="bindIcon" @click="collapseChage"></el-button>
        </el-col>
        <el-col :span="2">
            <el-dropdown class="el-dropdown-link">
                <span>欢迎您 {{loginInfo.userName}}<i class="el-icon-arrow-down el-icon--right"></i></span>
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
    import eventBus from '@/js/vue/eventBus';
    export default {
        name: 'HeaderTemplate',
        data() {
            return {
                activeIndex: "index",
                isCollapse: false,
                bindIcon: "el-icon-s-fold",
                loginInfo: {
                    userName: ""
                }
            }
        },
        components: {
            eventBus
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
                eventBus.$emit('isCollapse', this.isCollapse);
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
    .header {
        background-color: #545C64;
    }

    .btn-menu {
        font-size: 35px;
        background-color: #545C64;
        border-radius: 0px;
        border-width: 0px;
    }

    .el-dropdown-link {
        cursor: pointer;
        color: white;
        margin-top: 18px;
        float: right;
    }
</style>