<template>
    <div class="">
        <el-form :model="cardContentForm">
            {{currentCard.cardCode}}-{{currentCard.cardName}}
            <el-form-item label="卡片内容">
                <el-input v-model="cardContentForm.item"></el-input>
            </el-form-item>
            <el-form-item label="备注">
                <el-input v-model="cardContentForm.beizhu"></el-input>
            </el-form-item>
            <el-form-item label="区域等级">
            <el-radio-group v-model="cardContentForm.diORx">
              <el-radio label="0">地级</el-radio>
              <el-radio label="1">县级</el-radio>
            </el-radio-group>
          </el-form-item>
          <el-form-item>
            <el-button @click="submitCardContent">提交</el-button>
          </el-form-item>
            <!-- <el-form-item label="是否多用户">
                <el-switch v-model="_multisign"></el-switch>
            </el-form-item> -->
        </el-form>
    </div>
</template>

<script>
import {generalGet} from '../../network/general'
export default {
    name: 'addCardContent',
    props: {
        currentCard: 0
    },
    data() {
        return {
            cardContentForm: {
                cardCode: '',
                item: '',
                beizhu: '',
                cardName: '',
                score: '',//备用可不填写
                k50: '',//记录问卷达标标准1，比如问卷这一项90%以上是优秀则此处记录0.9，普通卡片不填
                k51: '',//记录问卷达标标准2，比如问卷这一项70%以上是合格则此处记录0.7，普通卡片不填
                multisign: 0,//是否多用户测评,默认为1（是）
                diORx: '0',
            }
        }
    },
    methods: {
        submitCardContent() {
            let that = this;
            if(this.cardContentForm.item.trim() != '' && this.cardContentForm.beizhu.trim() != '')
            {
                let data = {
                    url: '/api/SetCardContent',
                    params: {
                        cardCode: this.currentCard.cardCode,
                        item: this.cardContentForm.item,
                        beizhu: this.cardContentForm.beizhu,
                        cardName: this.currentCard.cardName,
                        diORx: this.cardContentForm.diORx
                    }
                }
                generalGet(data).then((res) => {
                    console.log(res);
                    if(res.msgCode == "OK") {
                        that.$notify({
                            title: '添加成功'
                        });
                        //清掉文本框内容
                        that.cardContentForm.beizhu = "",
                        that.cardContentForm.item = "",
                        that.$emit('closeDialog');
                    }
                })
            }else {
                this.$notify.error({
                    title: '请填写完整',
                    duration: 3000
                })
            }
            
        }
    },
    computed: {
        _multisign: {
            get() {
                return this.cardContentForm.multisign == 1 ? true : false;
            },
            set(val) {
                val == true ? this.cardContentForm.multisign = 1 : this.cardContentForm.multisign = 0
            }
        }
    },
}
</script>

<style scoped>

</style>