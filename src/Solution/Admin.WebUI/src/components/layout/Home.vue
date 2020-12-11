<template>
    <el-container>
        <el-aside><vtAside /></el-aside>
        <el-container>
            <el-header><vtHeader /></el-header>
            <vt-tabs></vt-tabs>
            <el-main><router-view /></el-main>
            <el-footer><vtFooter /></el-footer>
        </el-container>
    </el-container>
</template>

<script>
    import vtAside from './Aside.vue';
    import vtHeader from './Header.vue';
    import vtFooter from './Footer.vue';
    import vtTabs from './Tabs.vue';
    export default {
        name: 'HomeTemplate',
        data() {
            return {
                tagsList: [],
                collapse: false
            }
        },
        components: {
            vtAside,
            vtHeader,
            vtFooter,
            vtTabs
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
    /*左侧样式*/
    .el-aside {
        width: auto !important;
        height: 100vh;
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
            background: #FFAA25;
        }

    /*容器样式*/
    .el-container {
        height: 100vh;
    }

    /*右侧页眉*/
    .el-header {
        margin: 0px;
        padding: 0px;
        background-color: #545C64;
        height: 60px;
        line-height: 60px;
    }
    /*右侧主体*/
    .el-main {
        background-color: #F2F2F2;
        color: #333;
        text-align: center;
        margin: 0px;
        padding: 0px;
        /*width:calc(100vw - 300px);*/
    }
    /*右侧页尾*/
    .el-footer {
        background-color: #2F4050;
        color: white;
        text-align: center;
        height: 30px !important;
        line-height: 30px !important;
    }
</style>
