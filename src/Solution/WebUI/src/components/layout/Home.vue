<template>
    <el-container>
        <el-aside>
            <v-aside></v-aside>
        </el-aside>
        <el-container class="section-right">
            <el-header>
                <v-header></v-header>
            </el-header>
            <v-tabs></v-tabs>
            <el-main>
                <router-view></router-view>
            </el-main>
            <el-footer>
                <v-footer></v-footer>
            </el-footer>
        </el-container>
    </el-container>
</template>

<script>
    import vAside from './Aside.vue';
    import vHeader from './Header.vue';
    import vFooter from './Footer.vue';
    import vTabs from './Tabs.vue';
    export default {
        name: 'HomeTemplate',
        data() {
            return {
                tagsList: [],
                collapse: false
            }
        },
        components: {
            vAside,
            vHeader,
            vFooter,
            vTabs
        },
        created() {
        },
        mounted() {
        },
        methods: {
        }
    };
</script>

<!-- 添加"scoped"属性来限制CSS只应用到当前这个组件 lang="scss"使用sass写样式-->
<style scoped>
    .el-aside {
        width: auto !important;
        height: 100vh;
        background-color: #545C64;
    }
        /*滚动条整体样式*/
        .el-aside::-webkit-scrollbar {
            width: 0px;
            height: 0px;
            cursor: pointer;
        }
        /*滚动条里面小方块*/
        .el-aside::-webkit-scrollbar-thumb {
            background-color: #409EFF;
            border-radius: 6px;
        }
        /*滚动条里面轨道*/
        .el-aside::-webkit-scrollbar-track {
            background: #545C64;
        }

    .section-right {
        height: 100vh;
    }

    .el-main {
        background-color: #F2F2F2;
        color: #333;
        text-align: center;
        margin: 0px;
        padding: 0px;
    }

    .el-header {
        padding: 0px;
        background-color: #545C64;
    }

    .el-footer {
        background-color: #2F4050;
        color: white;
        text-align: center;
        height: 30px !important;
        line-height: 30px !important;
    }
</style>
