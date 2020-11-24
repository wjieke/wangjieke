/**
 * 分页模型数据
 * currentPage：当前页数，类型number，默认1
 * pageSizes：每页显示个数选择器的选项设置，类型number[]，默认[10, 20, 30, 40, 50, 100]
 * pageSize：每页显示条目个数，类型number，默认10
 * pagerCount：页码按钮的数量，当总页数超过该值时会折叠，类型number(大于等于 5 且小于等于 21 的奇数)，默认7
 * total：总条目数，类型number
 * */
export const paginationModel = {
    currentPage: 1,
    pageSizes: [5, 10, 15, 20, 50],
    pageSize: 10,
    pagerCount: 5,
    total: 100
}