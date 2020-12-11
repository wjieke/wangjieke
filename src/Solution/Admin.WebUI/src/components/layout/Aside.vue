<!--左侧模板文件-->
<template>
    <el-menu mode="vertical"
             background-color="#545c64"
             text-color="#fff"
             active-text-color="#66B1FF"
             :default-active="onRoutes"
             :collapse="isCollapse"
             :unique-opened="true"
             :collapse-transition="false"
             router>
        <menu-tree :menuList="menuList"></menu-tree>
    </el-menu>
</template>

<script>
    import { menus } from "@/js/data/menus.js";
    import { menuUrl } from "@/js/common/api.js";
    import menuTree from './Menu.vue'; //引入菜单模板
    export default {
        name: "AsideTemplate",
        data() {
            return {
                isCollapse: false,
                menuList: menus
            };
        },
        components: {
            menuTree
        },
        created: function () {
            var _this = this;
            this.$eventBus.$on("isCollapse", function (value) {
                _this.isCollapse = value;
            });
            //this.getMenu();
        },
        mounted: function () { },
        methods: {
            getMenu() {
                var _this = this;
                _this.$axios({
                    method: "get",
                    url: menuUrl.getNode,
                    params: { id: null }
                }).then(function (response) {
                    var retDatas = response.data;
                    _this.nodes = retDatas;
                    if (_this.menuList.length === 0 || _this.menuList === null) {
                        _this.menuList = menus;
                    }
                });
            }
        },
        computed: {
            onRoutes() {
                return this.$route.path.replace("/", "");
            }
        },
        watch: {}
    };
</script>

<style scoped>
    /*菜单ul样式*/
    .el-menu {
        height: 100vh;
        border: solid 0px white;
    }
        /*菜单折叠样式*/
        .el-menu:not(.el-menu--collapse) {
            min-width: 300px;
            width: auto !important;
        }
</style>
