<template>
    <div>
        <div class="flex-box">
            <span>问卷总分</span>
            <el-col :span="5">
            <el-input v-model="totalScore" placeholder="请输入总分"></el-input>
            </el-col>
        </div>
        <div class="flex-box">
            <span>测评类型</span>
        <el-dropdown split-button @command="handleChoseType">
            {{chosedType}}
            <el-dropdown-menu slot="dropdown">
                <div v-for="(item,index) in typeArr" :key="index">
                <el-dropdown-item :command="item">{{item}}</el-dropdown-item>
                </div>
            </el-dropdown-menu>
        </el-dropdown>
        </div>
    </div>
</template>

<script>
export default {
    name: 'addQuestionnaire',
    data() {
        return {
            chosedType: '请选择',
            typeArr: ['文明单位', '文明村镇', '文明社区', '文明校园', '少年宫', '未成年人'],
            totalScore: '',
        }
    },
    methods: {
        handleChoseType(e) {
            this.chosedType = e;
        }
    }
}
</script>

<style scoped>
    .flex-box {
        display: flex;
    }
</style>