<template>
    <div>
        <el-form :model="appraisalForm" :rules="formRule" ref="appraisalForm">
            <el-form-item label="测评名称" prop="appraisalName">
                <el-input clearable v-model="appraisalForm.appraisalName" placeholder="请输入测评名称"/>
            </el-form-item>
            <el-form-item label="总分" prop="totalScore">
                <el-input clearable v-model="appraisalForm.totalScore" placeholder="请输入总分"/>
            </el-form-item>
            <el-form-item label="类型">
                <el-radio-group v-model="appraisalForm.scoreType">
                    <el-radio label="问卷">问卷</el-radio>
                    <el-radio label="卡片">卡片</el-radio>
                </el-radio-group>
            </el-form-item>
            
            <el-form-item>
                <el-button @click="submitAppraisalForm('appraisalForm')">提交</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>

<script>
import {generalGet} from '../../network/general'
export default {
    name: 'addAppraiasal',
    data() {
        var checkAppraisalName = (rule, value, callback) => {
            if(!value) {
                return callback(new Error("测评名称不能为空"));
            }else {
                callback();
            }
        }
        var checkTotalScore = (rule, value, callback) => {
            if(!value) {
                return callback(new Error("总分不能为空"))
            }else if(parseFloat(value).toString() == "NaN"){
                return callback(new Error("请输入数字"));
            }else {
                callback();
            }
        }
        
        return {
            
            appraisalForm:{
                appraisalName: "测试",
                totalScore: "40",
                scoreType: "问卷",
            },
            formRule: {
                appraisalName: [
                    {validator: checkAppraisalName, triggle: "blur"}
                ],
                totalScore: [
                    {validator: checkTotalScore, triggle: "blur"}
                ],
            }
        }
    },
    methods: {
        submitAppraisalForm(appraisalForm) {
            let that = this;
            let appraisalNameData = {
                url: '/api/StartAppraisal',
                params: {AppraisalName: that.appraisalForm.appraisalName}
            }
            let totalScoreData = {
                url: '/api/SetSumScore',
                params: {score: that.appraisalForm.totalScore, socreType: that.appraisalForm.scoreType}
            }
            this.$refs[appraisalForm].validate((valid) => {
                if(!valid) {
                    console.log(valid)
                }else {
                    generalGet(appraisalNameData).then((res) => {
                        if(res.msgCode == "OK") {
                            console.log(res)
                            return generalGet(totalScoreData);
                        }
                    }).then((res2) => {
                        if(res2.msgCode == "OK") {
                            console.log(res2)
                            return generalGet(cardNameData);
                        }
                    }).catch(err => {
                        console.log(err)
                    })
                }
            })
        }
    }
}
</script>

<style scoped>
</style>