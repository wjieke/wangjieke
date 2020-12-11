<template>
    <el-row v-if="showTabs">
        <el-col :span="22">
            <el-tabs v-model="editableTabsValue" type="card" closable @tab-remove="removeTab" @tab-click="clickTabs">
                <el-tab-pane v-for="tab in editableTabs" :key="tab.name" :label="tab.title" :name="tab.name"></el-tab-pane>
            </el-tabs>
        </el-col>
        <el-col :span="2" class="col2">
            <el-dropdown @command="handleTabs">
                <el-button type="text">标签页选项卡<i class="el-icon-arrow-down el-icon--right"></i></el-button>
                <el-dropdown-menu slot="dropdown">
                    <el-dropdown-item command="other">关闭其他</el-dropdown-item>
                    <el-dropdown-item command="all">关闭所有</el-dropdown-item>
                </el-dropdown-menu>
            </el-dropdown>
        </el-col>
    </el-row>
</template>

<script>
    export default {
        name: 'TabsTemplate',
        data() {
            return {
                editableTabs: [],
                editableTabsValue: 'Index'
            }
        },
        methods: {
            //标签选项命令
            handleTabs(command) {
                command === 'other' ? this.closeOther() : this.closeAll();
            },
            // 关闭单个标签
            removeTab(targetName) {
                let tabs = this.editableTabs;
                let activeName = this.editableTabsValue;
                if (activeName === targetName) {
                    tabs.forEach((tab, index) => {
                        if (tab.name === targetName) {
                            let nextTab = tabs[index + 1] || tabs[index - 1];
                            if (nextTab) {
                                activeName = nextTab.name;
                            }
                        }
                    });
                }
                this.editableTabsValue = activeName;
                this.editableTabs = tabs.filter(tab => tab.name !== targetName);
                this.$router.push("/" + activeName);
                if (this.editableTabs.length === 0) {
                    this.$router.push('/Index');
                }
                if (this.editableTabs.length === 1 && this.editableTabs[0].name === "Index") {
                    this.$router.push('/Index');
                }
            },
            // 关闭全部标签
            closeAll() {
                this.editableTabs = [];
                this.$router.push('/Index');
            },
            // 关闭其他标签
            closeOther() {
                const curItem = this.editableTabs.filter(item => {
                    return item.path === this.$route.fullPath;
                })
                this.editableTabs = curItem;
            },
            // 设置标签
            setTabs(route) {
                var _this = this;
                const isExist = _this.editableTabs.some(item => {
                    return item.path === route.fullPath;
                })
                if (!isExist) {
                    _this.editableTabs.push({
                        title: route.meta.title,
                        name: route.name,
                        path: route.fullPath
                    });
                }
                _this.editableTabsValue = route.name;
            },
            //点击标签
            clickTabs(tab) {
                this.$router.push("/" + tab.name);
            }
        },
        computed: {
            //是否显示组件
            showTabs() {
                return this.editableTabs.length > 0;
            }
        },
        watch: {
            //监听菜单点击的路由
            $route(newValue) {
                this.setTabs(newValue);
            }
        },
        created() {
            //初始化tabs组件
            this.setTabs(this.$route);
        }
    }
</script>

<style scoped>
    .col2 {
        text-align: center;
        border-bottom: 1px solid #DCDFE6;
    }
</style>
