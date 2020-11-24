发布API 到阿里云服务器，需要添加安全组规则，配置端口
需要下载：dotnet-hosting-3.1.3-win.exe
		  dotnet-sdk-3.1.201-win-x64.exe
发布部署在IIS上

项目依赖结构关系(从上层到下层依赖)：WebUI -> WebAPI -> Service -> IService -> DbAccess -> Library -> Model
