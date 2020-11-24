<template>
    <div class="error">
        <h1>404 您访问的页面弄丢了，555...</h1>
    </div>
</template>

<script>
    export default {
        name: 'ErrorTemplate',
        data() {
            return {

            }
        },
        components: {},
        created: function () { },
        mounted: function () { },
        methods: {},
        computed: {},
        watch: {}
    };
</script>

<style scoped>
    .error {
        position: absolute;
        width: 100%;
        height: 100%;
        display: flex;
        justify-content: center;
        align-items: center;
        color: #676a6c;
    }
</style>