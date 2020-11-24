<template>
    <el-menu class="el-menu-vertical-aside"
             mode="vertical"
             background-color="#545c64"
             text-color="#fff"
             active-text-color="#66B1FF"
             :default-active="onRoutes"
             :collapse="isCollapse"
             :unique-opened="true"
             :collapse-transition="false"
             router>
        <template v-for="item in nodes">
            <template v-if="item.children">
                <el-submenu :index="item.index" :key="item.index">
                    <template slot="title">
                        <i :class="item.menuIcon"></i>
                        <span slot="title">{{ item.menuName }}</span>
                    </template>
                    <template v-for="subItem in item.children">
                        <el-submenu v-if="subItem.children" :index="subItem.index" :key="subItem.index">
                            <template slot="title">
                                {{ subItem.menuName }}
                            </template>
                            <el-menu-item v-for="(threeItem, i) in subItem.children"
                                          :key="i"
                                          :index="threeItem.index">{{ threeItem.menuName }}</el-menu-item>
                        </el-submenu>
                        <el-menu-item v-else
                                      :index="subItem.index"
                                      :key="subItem.index">{{ subItem.menuName }}</el-menu-item>
                    </template>
                </el-submenu>
            </template>
            <template v-else>
                <el-menu-item :index="item.index" :key="item.index">
                    <i :class="item.menuIcon"></i>
                    <span slot="title">{{ item.menuName }}</span>
                </el-menu-item>
            </template>
        </template>
    </el-menu>
</template>

<script>
    import eventBus from "@/js/vue/eventBus";
    import { menus } from "@/js/data/menus.js";
    import { menuUrl } from "@/js/common/api.js";
    export default {
        name: "AsideTemplate",
        data() {
            return {
                isCollapse: false,
                nodes: menus
            };
        },
        components: {
            eventBus
        },
        created: function () {
            var _this = this;
            eventBus.$on("isCollapse", function (value) {
                _this.isCollapse = value;
            });
            //this.getMenu();
        },
        mounted: function () { },
        methods: {
            getMenu() {
                var _this = this;
                _this
                    .$axios({
                        method: "get",
                        url: menuUrl.getNode,
                        params: { id: null }
                    })
                    .then(function (response) {
                        var retDatas = response.data;
                        _this.nodes = retDatas;
                        if (_this.nodes.length === 0 || _this.nodes === null) {
                            _this.nodes = menus;
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
    .el-menu-vertical-aside {
        height: 100vh;
        border: solid 0px white;
    }

        .el-menu-vertical-aside:not(.el-menu--collapse) {
            min-width: 250px;
            width: auto !important;
        }

    .el-menu-item.is-active {
        color: #409EFF !important;
        border-right: 0px solid #409EFF;
    }
</style>
