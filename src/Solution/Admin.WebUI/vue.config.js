module.exports = {
    publicPath: './',
    lintOnSave: false,//禁用Eslint 语法检查
    outputDir: process.env.NODE_ENV === "development" ? 'dist_dev' : 'dist'
}