(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-dfb39b4a"],{"11e9":function(e,t,a){var n=a("52a7"),o=a("4630"),l=a("6821"),i=a("6a99"),r=a("69a8"),s=a("c69a"),c=Object.getOwnPropertyDescriptor;t.f=a("9e1e")?c:function(e,t){if(e=l(e),t=i(t,!0),s)try{return c(e,t)}catch(a){}if(r(e,t))return o(!n.f.call(e,t),e[t])}},"3b2b":function(e,t,a){var n=a("7726"),o=a("5dbc"),l=a("86cc").f,i=a("9093").f,r=a("aae3"),s=a("0bfb"),c=n.RegExp,d=c,u=c.prototype,g=/a/g,h=/a/g,p=new c(g)!==g;if(a("9e1e")&&(!p||a("79e5")((function(){return h[a("2b4c")("match")]=!1,c(g)!=g||c(h)==h||"/a/i"!=c(g,"i")})))){c=function(e,t){var a=this instanceof c,n=r(e),l=void 0===t;return!a&&n&&e.constructor===c&&l?e:o(p?new d(n&&!l?e.source:e,t):d((n=e instanceof c)?e.source:e,n&&l?s.call(e):t),a?this:u,c)};for(var f=function(e){e in c||l(c,e,{configurable:!0,get:function(){return d[e]},set:function(t){d[e]=t}})},m=i(d),y=0;m.length>y;)f(m[y++]);u.constructor=c,c.prototype=u,a("2aba")(n,"RegExp",c)}a("7a56")("RegExp")},"5dbc":function(e,t,a){var n=a("d3f4"),o=a("8b97").set;e.exports=function(e,t,a){var l,i=t.constructor;return i!==a&&"function"==typeof i&&(l=i.prototype)!==a.prototype&&n(l)&&o&&o(e,l),e}},8206:function(e,t,a){"use strict";a.d(t,"a",(function(){return n}));var n={currentPage:1,pageSizes:[5,10,15,20,50],pageSize:10,pagerCount:5,total:100}},"8b97":function(e,t,a){var n=a("d3f4"),o=a("cb7c"),l=function(e,t){if(o(e),!n(t)&&null!==t)throw TypeError(t+": can't set as prototype!")};e.exports={set:Object.setPrototypeOf||("__proto__"in{}?function(e,t,n){try{n=a("9b43")(Function.call,a("11e9").f(Object.prototype,"__proto__").set,2),n(e,[]),t=!(e instanceof Array)}catch(o){t=!0}return function(e,a){return l(e,a),t?e.__proto__=a:n(e,a),e}}({},!1):void 0),check:l}},9093:function(e,t,a){var n=a("ce10"),o=a("e11e").concat("length","prototype");t.f=Object.getOwnPropertyNames||function(e){return n(e,o)}},a942:function(e,t,a){},ab42:function(e,t,a){"use strict";var n=a("a942"),o=a.n(n);o.a},c810:function(e,t,a){"use strict";a.r(t);var n=function(){var e=this,t=e.$createElement,a=e._self._c||t;return a("el-container",[a("el-header",[a("el-card",{staticClass:"search-card"},[a("el-form",{staticClass:"search-form-inline",attrs:{inline:!0,model:e.searchModel}},[a("el-form-item",{attrs:{label:"关键字"}},[a("el-input",{attrs:{placeholder:"名称",clearable:""},model:{value:e.searchModel.menuName,callback:function(t){e.$set(e.searchModel,"menuName",t)},expression:"searchModel.menuName"}})],1),a("el-form-item",{attrs:{label:"终端类型"}},[a("el-select",{attrs:{placeholder:"请选择"},model:{value:e.searchModel.clientCategory,callback:function(t){e.$set(e.searchModel,"clientCategory",t)},expression:"searchModel.clientCategory"}},e._l(e.searchModel.clientCategorys,(function(e){return a("el-option",{key:e.value,attrs:{label:e.label,value:e.value}})})))],1),a("el-form-item",{attrs:{label:"数据状态"}},[a("el-select",{attrs:{placeholder:"请选择"},model:{value:e.searchModel.dataState,callback:function(t){e.$set(e.searchModel,"dataState",t)},expression:"searchModel.dataState"}},e._l(e.searchModel.dataStates,(function(e){return a("el-option",{key:e.value,attrs:{label:e.label,value:e.value}})})))],1),a("el-form-item",[a("el-button",{attrs:{type:"primary",icon:"el-icon-search"},on:{click:e.search}},[e._v("搜索")])],1)],1)],1)],1),a("el-container",[a("el-aside",[a("el-card",{staticClass:"tree-card"},[a("el-tree",{ref:"tree",staticClass:"filter-tree",attrs:{data:e.trees,props:e.treeProps,"default-expand-all":"","expand-on-click-node":!1,"filter-node-method":e.filterNode},on:{"node-click":e.handleNodeClick}})],1)],1),a("el-main",[a("el-card",{staticClass:"operation-card"},[a("el-breadcrumb",{attrs:{separator:"/"}},e._l(e.paths,(function(t,n){return a("el-breadcrumb-item",{key:n,nativeOn:{click:function(a){a.preventDefault(),e.onLinkObjectData(t.id)}}},[a("a",{attrs:{href:"#"}},[e._v(e._s(t.menuName))])])}))),a("el-button-group",[a("el-button",{attrs:{type:"primary",icon:"el-icon-circle-plus-outline"},on:{click:e.add}},[e._v("添加")]),a("el-button",{attrs:{type:"primary",icon:"el-icon-edit"},on:{click:e.mod}},[e._v("修改")]),a("el-button",{attrs:{type:"primary",icon:"el-icon-delete"},on:{click:e.del}},[e._v("删除")])],1)],1),a("el-card",{staticClass:"table-card"},[a("el-table",{staticStyle:{width:"100%"},attrs:{data:e.datas,"max-height":"424",stripe:"","default-sort":{prop:"id",order:"descending"}},on:{"selection-change":e.handleSelectionChange}},[a("el-table-column",{attrs:{fixed:"",type:"selection",width:"55"}}),a("el-table-column",{attrs:{prop:"id",label:"菜单编号",width:"120",sortable:""}}),a("el-table-column",{attrs:{prop:"menuName",label:"菜单名称","show-overflow-tooltip":"",width:""},scopedSlots:e._u([{key:"default",fn:function(t){return[a("el-button",{attrs:{type:"text",size:"small"},on:{click:function(a){e.getListById(t.row)}}},[e._v(e._s(t.row.menuName))])]}}])}),a("el-table-column",{attrs:{prop:"terminalCategory",label:"终端类别",width:"120",sortable:""}}),a("el-table-column",{attrs:{prop:"sort",label:"排序",width:"120",sortable:""}}),a("el-table-column",{attrs:{prop:"dataState",label:"数据状态",fixed:"right"},scopedSlots:e._u([{key:"default",fn:function(t){return[a("span",{domProps:{innerHTML:e._s(t.row.dataState)}})]}}])})],1)],1),a("el-card",{staticClass:"pagination-card"},[a("el-pagination",{attrs:{"current-page":e.pagingModel.currentPage,"page-sizes":e.pagingModel.pageSizes,"page-size":e.pagingModel.pageSize,"pager-count":e.pagingModel.pagerCount,layout:"total, sizes, prev, pager, next, jumper",total:e.pagingModel.total},on:{"size-change":e.handleSizeChange,"current-change":e.handleCurrentChange}})],1)],1)],1),a("el-dialog",{attrs:{title:"菜单信息",visible:e.dialogFormVisible},on:{"update:visible":function(t){e.dialogFormVisible=t}}},[a("el-form",{ref:"editForm",attrs:{inline:!1,model:e.entityModel,rules:e.rules,"label-width":"auto"}},[a("el-row",{attrs:{gutter:0,type:"flex",justify:"center"}},[a("el-col",{attrs:{span:24}},[a("el-form-item",{attrs:{label:"菜单名称",prop:"menuName"}},[a("el-input",{staticStyle:{width:"200px",float:"left"},attrs:{placeholder:"名称",autocomplete:"off",clearable:""},model:{value:e.entityModel.menuName,callback:function(t){e.$set(e.entityModel,"menuName",t)},expression:"entityModel.menuName"}})],1),a("el-form-item",{attrs:{label:"所属菜单"}},[a("el-cascader",{staticStyle:{width:"200px",float:"left"},attrs:{options:e.nodes,"change-on-select":"",filterable:"",clearable:!0,props:e.nodeProps,"expand-trigger":"hover"},on:{change:e.handleItemChange},model:{value:e.entityModel.ids,callback:function(t){e.$set(e.entityModel,"ids",t)},expression:"entityModel.ids"}})],1),a("el-form-item",{attrs:{label:"客户端类别"}},[a("el-select",{staticStyle:{width:"200px",float:"left"},attrs:{placeholder:"请选择"},model:{value:e.entityModel.clientCategory,callback:function(t){e.$set(e.entityModel,"clientCategory",t)},expression:"entityModel.clientCategory"}},e._l(e.entityModel.clientCategorys,(function(e){return a("el-option",{key:e.value,attrs:{label:e.label,value:e.value}})})))],1),a("el-form-item",{attrs:{label:"菜单状态"}},[a("el-select",{staticStyle:{width:"200px",float:"left"},attrs:{placeholder:"请选择"},model:{value:e.entityModel.dataState,callback:function(t){e.$set(e.entityModel,"dataState",t)},expression:"entityModel.dataState"}},e._l(e.entityModel.dataStates,(function(e){return a("el-option",{key:e.value,attrs:{label:e.label,value:e.value}})})))],1),a("el-form-item",{attrs:{label:"菜单排序",prop:"sort"}},[a("el-input-number",{staticStyle:{width:"200px",float:"left"},attrs:{placeholder:"排序",min:0,max:10,label:"排序"},model:{value:e.entityModel.sort,callback:function(t){e.$set(e.entityModel,"sort",t)},expression:"entityModel.sort"}})],1),a("el-form-item",{attrs:{label:"备注"}},[a("el-input",{staticStyle:{width:"200px",float:"left"},attrs:{type:"textarea",rows:2,placeholder:"请输入内容"},model:{value:e.entityModel.remark,callback:function(t){e.$set(e.entityModel,"remark",t)},expression:"entityModel.remark"}})],1)],1)],1)],1),a("div",{staticClass:"dialog-footer",attrs:{slot:"footer"},slot:"footer"},[a("el-button",{on:{click:function(t){e.dialogFormVisible=!1}}},[e._v("取 消")]),a("el-button",{on:{click:function(t){e.resetForm("editForm")}}},[e._v("重置")]),a("el-button",{attrs:{type:"primary"},on:{click:function(t){e.save("editForm")}}},[e._v("保存并添加")]),a("el-button",{attrs:{type:"primary"},on:{click:function(t){e.savec("editForm")}}},[e._v("保存并关闭")])],1)],1)],1)},o=[],l=(a("ac6a"),a("cc86")),i=a("8206"),r=a("806d"),s={name:"MenuTemplate",data:function(){return{pagingModel:i["a"],searchModel:{parentId:null,menuName:""},entityModel:{id:0,parentId:null,ids:[],menuName:"",sort:0},rules:{menuName:[{required:!0,message:"请输入名称",trigger:"blur"}]},treeProps:{id:"id",label:"menuName",children:"children"},nodeProps:{value:"id",label:"menuName",children:"children",disabled:"disabled",expandTrigger:"hover",checkStrictly:"false"},nodes:[],trees:[],datas:[],paths:[],multipleSelection:[],inputIsDisabled:!1,dialogFormVisible:!1,filterText:"",selectlOptions:[],value:""}},components:{},created:function(){},mounted:function(){this.getTree(),this.getNode(),this.getPath(),this.getPage(),this.getDataState(),this.getClientCategory()},computed:{},methods:{add:function(){this.entityModel.id=0,this.entityModel.parentId=null,this.entityModel.ids=[],this.entityModel.menuName="",this.entityModel.sort=0,this.dialogFormVisible=!0},mod:function(){var e=this,t=this.getIds();0===t.length||t.length>1?this.$message({type:"info",message:"请选择一条数据修改"}):this.$axios({method:"get",url:r["g"].getInfo,params:{id:t[0]}}).then((function(t){e.entityModel=t.data,e.dialogFormVisible=!0}))},del:function(){var e=this,t=this,a=t.getIds();0!==a.length?this.$confirm("确认删除该数据吗, 是否继续?","提示",{confirmButtonText:"确定",cancelButtonText:"取消",type:"warning"}).then((function(){t.$axios({method:"delete",url:r["g"].delInfo,data:a}).then((function(e){1===e.data.resultState?t.$message({showClose:!0,message:"提示，删除成功",type:"success",duration:1e3,onClose:function(){t.getPage()}}):t.$message({type:"info",message:"删除失败！"+e.data.message})}))})).catch((function(){e.$message({type:"info",message:"已取消删除"})})):this.$message({type:"info",message:"请至少选择一条数据删除"})},savec:function(e){this.save(e),this.dialogFormVisible=!1},save:function(e){var t=this.entityModel.ids,a=t[t.length-1];a!==this.entityModel.id&&(this.entityModel.parentId=a);var n=this,o="",l="";0===this.entityModel.id?(o=r["g"].addInfo,l="post"):(o=r["g"].modInfo,l="put"),this.$refs[e].validate((function(e){if(!e)return!1;n.$axios({method:l,url:o,data:n.entityModel}).then((function(e){1===e.data.resultState?n.$message({showClose:!0,message:"提示，保存成功",type:"success",duration:1e3,onClose:function(){n.getPage(),n.getTree()}}):n.$message({type:"info",message:"失败！"+e.data.message})}))}))},search:function(){this.pagingModel.currentPage=1,this.getPage()},resetForm:function(e){this.$refs[e].resetFields()},handleSizeChange:function(e){this.pagingModel.pageSize=e},handleCurrentChange:function(e){this.pagingModel.currentPage=e},handleSelectionChange:function(e){this.multipleSelection=e},getPage:function(){var e=this,t={pageIndex:this.pagingModel.currentPage,pageSize:this.pagingModel.pageSize,parentId:this.searchModel.parentId,menuName:this.searchModel.menuName};e.$axios({method:"post",url:r["g"].getPage,data:t}).then((function(t){var a=t.data;null!=a.datas?a.datas.forEach((function(e){switch(null!==e.createTime?e.createTime=Object(l["a"])(e.createTime):e.createTime=null,null!==e.modifyTime?e.modifyTime=Object(l["a"])(e.modifyTime):e.modifyTime=null,e.dataState){case 1:e.dataState='<span style="color:green;">启用</span>';break;case 2:e.dataState='<span style="color:gray;">停用</span>';break;default:}})):a.datas=[],e.datas=a.datas,e.pagingModel.total=a.total}))},getIds:function(){var e=this.multipleSelection,t=[];return e.map((function(e){t.push(e.id)})),t},getNode:function(){var e=this;e.$axios({method:"get",url:r["g"].getNode,params:{id:null}}).then((function(t){var a=t.data;e.nodes=a}))},getTree:function(){var e=this;e.$axios({method:"get",url:r["g"].getTree}).then((function(t){var a=t.data;e.trees=a}))},getPath:function(){var e=this;e.$axios({method:"get",url:r["g"].getPath,params:{id:e.searchModel.parentId}}).then((function(t){e.paths=t.data}))},handleItemChange:function(e){e.length>1?this.isDisabled=!0:this.isDisabled=!1},getListById:function(e){var t=e.id;this.searchModel.parentId=t,this.pagingModel.currentPage=1,this.getPage(),this.getPath()},getObjectDataById:function(e){var t=e.id;this.searchModel.id=t,this.getPage(),this.getPath()},onLinkObjectData:function(e){0===e&&(e=null),this.searchModel.parentId=e,this.searchModel.menuName="",this.getPage(),this.getPath()},filterNode:function(e,t){return!e||-1!==t.menuName.indexOf(e)},handleNodeClick:function(e){this.onLinkObjectData(e.id)},getDataState:function(){var e=this;this.$axios({method:"get",url:r["e"].getDataState}).then((function(t){var a=t.data;e.searchModel.dataStates=a,e.entityModel.dataStates=a,e.searchModel.dataState=a[0].value,e.entityModel.dataState=a[0].value}))},getClientCategory:function(){var e=this;this.$axios({method:"get",url:r["e"].getClientCategory}).then((function(t){var a=t.data;e.searchModel.clientCategorys=a,e.entityModel.clientCategorys=a,e.searchModel.clientCategory=a[0].value,e.entityModel.clientCategory=a[0].value}))}},watch:{pagingModel:{deep:!0,handler:function(){this.getPage()}},filterText:function(e){this.$refs.tree.filter(e)}}},c=s,d=(a("ab42"),a("2877")),u=Object(d["a"])(c,n,o,!1,null,"8d5ee398",null);t["default"]=u.exports},cc86:function(e,t,a){"use strict";a.d(t,"a",(function(){return n}));a("3b2b"),a("a481");var n=function(e){var t=new Date(e),a=t.getFullYear(),n=t.getMonth()+1<10?"0"+(t.getMonth()+1):t.getMonth()+1,o=t.getDate()<10?"0"+t.getDate():t.getDate(),l=t.getHours()<10?"0"+t.getHours():t.getHours(),i=t.getMinutes()<10?"0"+t.getMinutes():t.getMinutes(),r=t.getSeconds()<10?"0"+t.getSeconds():t.getSeconds();return a+"-"+n+"-"+o+" "+l+":"+i+":"+r}}}]);
//# sourceMappingURL=chunk-dfb39b4a.e887573e.js.map