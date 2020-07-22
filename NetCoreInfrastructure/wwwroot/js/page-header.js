/**
 * 页头
 */
Vue.component("page-header",
    {
        props: ["title"],
        data: function () {
            return {
            };
        },
        methods: {
            back: function () {
                //向上返回一级
                this.$emit('close');
            }
        },
        template: `<el-page-header v-on:back="back" style="display: flex;justify-content: flex-start;align-items: center;background: #FFF;height: 30px;background-color:#1867c0 !important;color: white;height: 50px;box-shadow: 0px 3px 5px -1px rgba(0,0,0,0.2), 0px 6px 10px 0px rgba(0,0,0,0.14), 0px 1px 18px 0px rgba(0,0,0,0.12) !important;margin-bottom: 0px;">
        <template slot="content">
            <span style="color: white">{{title}}</span>
        </template>
    </el-page-header>`
    }
);