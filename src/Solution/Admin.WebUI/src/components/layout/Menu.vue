<template>
    <!--fragment 可以同时存在多个兄弟节点，且自身不会被渲染-->
    <fragment>
        <template v-for="item in menuList">
            <el-submenu v-if="item.children" :key="item.index" :index="item.index">
                <template slot="title">
                    <i :class="item.menuIcon"></i>
                    <span slot="title">{{ item.menuName}}</span>
                </template>
                <menu-tree :menuList="item.children"></menu-tree>
            </el-submenu>
            <el-menu-item v-else :index="item.index" :key="item.index">
                <i :class="item.menuIcon"></i>
                <span>{{item.menuName}}</span>
            </el-menu-item>
        </template>
    </fragment>
</template>

<script>
    export default {
        name: 'menu-tree',//模板名称,也可以直接写组件文件名称
        data() {
            return {}
        },
        beforeMount() { },
        created() { },
        mounted() { },
        props: ['menuList'],
        methods: {},
        computed: {},
        watch: {}
    }
</script>

<style scoped>
</style>