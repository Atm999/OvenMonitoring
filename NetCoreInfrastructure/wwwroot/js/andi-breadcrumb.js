/**
 * 多层页面导航栏
 */
Vue.component("andi-breadcrumb",
    {
        props: ["value","paths"],
        data: function () {
            var tempPaths = [];
            if (this.paths) {
                tempPaths = this.paths;
            }
            return {
                innerPaths: tempPaths
            };
        },
        methods: {
            change: function (newvalue) {
                if ('pushState' in history) {
                    let url = window.location.pathname;
                    //window.location.replace(url + "?path=" + newvalue.Path);
                    //window.history.pushState({ Path: getQueryVariable("path")}, "", url + "?path="+newvalue.Path);
                }
                
                this.$emit('input', newvalue);
            },
            select: function(info) {
                //选择指定的路径
                //判定选中路径的序号
                //删除其后的路径
                var index = _.findIndex(this.innerPaths, function (value) { return value.Path === info.Path; });
                if (index < 0) {
                    return;
                }
                this.innerPaths = _.slice(this.innerPaths, 0, index + 1);
                this.change(_.last(this.innerPaths));
            },
            add: function(info) {
                this.innerPaths.push(info);
                //设定信息的数据
                this.change(info);
            },
            back: function() {
                //向上返回一级
                if (this.innerPaths.length < 2) {
                    return;
                }

                this.innerPaths.pop();
                this.change(_.last(this.innerPaths));
            }


        },
        template: `<div style="display: flex;justify-content: space-between;align-items: center;border-bottom: 1px solid rgba(204,204,204,.8);padding-left: 15px;flex-direction: row;align-content: flex-start;background: #FFF;height: 30px;background-color:#1867c0 !important;color: white;height: 50px;box-shadow: 0px 3px 5px -1px rgba(0,0,0,0.2), 0px 6px 10px 0px rgba(0,0,0,0.14), 0px 1px 18px 0px rgba(0,0,0,0.12) !important;margin-bottom: 0px;" v-if="innerPaths.length>1">
        <el-breadcrumb separator-class="el-icon-arrow-right">
            <el-breadcrumb-item v-for="(item,index) in innerPaths" :key="index"><a v-on:click="select(item)" style="color:white">{{item.Name}}</a></el-breadcrumb-item>
        </el-breadcrumb>
        <el-popover
                            placement="top-start"
                            trigger="hover"
                            style="margin-right:15px;"
                            content="返回上级页面">
                            <el-button slot="reference" type="text" style="color: white;" v-on:click="back"><i class="fa fa-arrow-circle-left fa-lg"></i></el-button>
                          </el-popover>
        </div>`
    }
);

function getQueryVariable(variable) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    if (!vars) {
        vars = [query];
    }
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] === variable) { return pair[1]; }
    }
    return (false);
}