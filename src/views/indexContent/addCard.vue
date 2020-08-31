<template>
    <div>
        <el-form :model="cardForm" :rules="formRule" ref="cardForm">
            <el-form-item label="卡片名称" prop="cardName">
                <el-input v-model="cardForm.cardName" placeholder="请输入卡片名称"></el-input>
            </el-form-item>
            <el-form-item label="卡片编号" prop="cardCode">
                <el-input v-model="cardForm.cardCode" placeholder="请输入卡片编号"></el-input>
            </el-form-item>
            <el-form-item label="卡片选项数量" prop="cardItemCount">
                <el-input v-model="cardForm.cardItemCount" placeholder="请输入卡片选项数量"></el-input>
            </el-form-item>
            <el-form-item label="区域">
                <el-radio-group v-model="cardForm.diORx">
                    <el-radio label="0">地级</el-radio>
                    <el-radio label="1">县级</el-radio>
                </el-radio-group>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="submitCard('cardForm')">提交</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>

<script>
import {generalGet} from '../../network/general'
export default {
    name: 'addCard',
    data() {
        var checkCardName = (rule, value, callback) => {
            if(!value) {
                return callback(new Error("卡片名称不能为空"))
            }else {
                callback();
            }
        }
        var checkCardCode = (rule, value, callback) => {
            if(!value) {
                return callback(new Error("卡片编号不能为空"))
            }else {
                callback();
            }
        }
        var checkCardItemCount = (rule, value, callback) => {
            if(!value) {
                return callback(new Error("卡片选项数量不能为空"))
            }else if(parseFloat(value).toString() == "NaN"){
                return callback(new Error("请输入数字"));
            }else {
                callback();
            }
        }
        return {
            cardForm: {
                cardName: '测试卡片名',
                cardCode: 'K0202',
                cardItemCount: '2',
                diORx: '0'
            },
            formRule: {
                cardName: [
                    {validator: checkCardName, triggle: "blur"}
                ],
                cardCode: [
                    {validator: checkCardCode, triggle: "blur"}
                ],
                cardItemCount: [
                    {validator: checkCardItemCount, triggle: "blur"}
                ]
            }
            
        }
    },
    props: {
        currentAppraisalCode: String
    },
    methods: {
        submitCard(cardForm) {
            let that = this;
            this.$refs[cardForm].validate((valid) => {
                let that = this;
                if(!valid) {
                    console.log(valid)
                }else {
                    const data = {
                        url: '/api/SetCardList',
                        params: {
                            cardName: that.cardForm.cardName,
                            cardCode: that.cardForm.cardCode,
                            cardItemCount: that.cardForm.cardItemCount,
                            diORx: that.cardForm.diORx
                        }
                    }
            
                    generalGet(data).then((res) => {
                        console.log(res)
                        if(res) {
                            if(res.errorMsg == '成功') {
                                that.$notify({
                                    title: ' 添加卡片成功',
                                    type: 'success'
                                });
                            }else {
                                that.$notify.error({
                                    title: res.errorMsg,
                                })
                            }
                        }
                    })
                }
            })
            
        }
    
    }
}
</script>

<style scoped>

</style>