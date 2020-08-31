<template>
    <div>
        <el-table :data="cardListTable" highlight-current-row height="82vh" border>
            <el-table-column type="index" :index="indexMethod" label="序号"></el-table-column>
            <el-table-column label="测评编码" prop="appraisalCode"></el-table-column>
            <el-table-column label="卡片编码" prop="cardCode"></el-table-column>
            <el-table-column label="卡片选项总数" prop="cardItemCount"></el-table-column>
            <el-table-column label="卡片名称" prop="cardName"></el-table-column>
            <el-table-column label="区域等级">
                <template slot-scope="scope">
                    {{scope.row.diORx=='0'?'地级':'县级'}}
                </template>
            </el-table-column>
            <el-table-column label="操作" v-if="$store.state.loginInfo.type!=2">
                <template slot-scope="scope">
                    <el-button @click="addCardContent(scope.row)" size="small">添加内容</el-button>
                </template>
            </el-table-column>
        </el-table>
        <el-dialog :visible.sync="addCardContentDialog" width="40%" modal title="添加卡片内容">
            <addCardContent :currentCard="currentCard" @closeDialog="closeAddCardContent"/>
        </el-dialog>
    </div>
</template>

<script>
import global from '../../network/global_variable'
import {generalGet} from '../../network/general'

import addCardContent from './addCardContent'
export default {
    name: 'cardList',
    components: {
        addCardContent
    },
    data() {
        return {
            cardListTable: [],
            addCardContentDialog: false,
            currentCard: {}
        }
    },
    methods: {
        //加载数据
        loadCardListTable() {
            let that = this;
            let data = {
                url: '/api/ShowCardList'
            }
            generalGet(data).then((res) => {
                console.log(res);
                that.cardListTable = res.data
            })
            generalGet({url: '/api/ShowCardContent', params: {AppraisalCode: 7}}).then((res) => {
                console.log(res)
            })
        },
        //添加卡片内容
        addCardContent(row) {
            this.addCardContentDialog = true;
            this.currentCard = row;
        },
        //关闭添加
        closeAddCardContent() {
            this.addCardContentDialog = false;
        },
        //序号
        indexMethod(index) {
            return index + 1
        }
    },
    mounted() {
        this.loadCardListTable();
    },
}
</script>

<style scoped>

</style>