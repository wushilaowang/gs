<template>
    <div>
        <el-form :model="userForm">
            <el-form-item label="账号">
                <el-input v-model="userForm.Account" placeholder="请输入账号"></el-input>
            </el-form-item>
            <el-form-item label="密码">
                <el-input v-model="userForm.Password" placeholder="请输入密码"></el-input>
            </el-form-item>
            <el-form-item label="姓名">
                <el-input v-model="userForm.RealName" placeholder="请输入姓名"></el-input>
            </el-form-item>
            <el-form-item label="区域名称">
                <el-dropdown @command="choseDropDown" placement="top">
                    <span  class="el-dropdown-link">
                        {{choseDistrict.text}}<i class="el-icon-arrow-down el-icon--right"></i>
                    </span>
                    <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item v-for="(item,index) in district" :key="index" :command="item.districtCode+'+'+item.districtName">{{item.districtName}}</el-dropdown-item>
                    </el-dropdown-menu>
                </el-dropdown>
            </el-form-item>
            <el-form-item label="类型">
                <el-radio-group v-model="userForm.Type">
                    <el-radio label="0">管理员</el-radio>
                    <el-radio label="1">测评带队</el-radio>
                    <el-radio label="2">测评成员</el-radio>
                </el-radio-group>
            </el-form-item>
            <el-form-item label="地区">
                <el-radio-group v-model="userForm.diORx">
                    <el-radio label="0">地级</el-radio>
                    <el-radio label="1">县级</el-radio>
                </el-radio-group>
            </el-form-item>
            <el-form-item>
                <el-button type="primary" @click="submitUserForm">提交</el-button>
            </el-form-item>
        </el-form>
    </div>
</template>

<script>
import {generalGet} from '../../network/general'
export default {
    name: 'user',
    data() {
        return {
            userForm: {
                Account: '',
                Password: '',
                RealName: '',
                DistrictCode: '',
                DistrictName: '',
                Type: "2",
                diORx: "0",
            },
            district: [],
            choseDistrict: {
                text: '请选择区域',
                value: 0
            },
        }
    },
    methods: {
        //选取区域
        choseDropDown(value) {
            console.log(value);
            let valueArr = value.split('+');
            this.choseDistrict.text = valueArr[1];
            this.choseDistrict.value = valueArr[0];
        },
        //获取区域数据
        getDistrict() {
            let that = this;
            generalGet({url: '/api/ShowAlldistrict'}).then((res) => {
                console.log(res);
                if(res.msgCode == 'OK') {
                    this.district = res.data
                }
            })
        },
        //注册
        submitUserForm() {
            let that = this;
            if(this.userForm.Account == '') {
                this.$notify.error({
                    title: '请输入账号'
                });
            }else if(this.userForm.Password == '') {
                this.$notify.console.error({
                    title: '请输入密码'
                });
            }else if(this.userForm.Realname == '') {
                this.$notify.error({
                    title: '请输入姓名'
                });
            }else if(this.choseDistrict.value == 0) {
                this.$notify.error({
                    title: '请选择区域'
                });
            }else {
                let data = {
                    url: '/api/SetUser',
                    params: {
                        Account: this.userForm.Account,
                        Password: this.userForm.Password,
                        RealName: this.userForm.RealName,
                        DistrictCode: this.userForm.Type == 2 ? this.choseDistrict.value : this.choseDistrict.value + ',0',
                        DistrictName: this.choseDistrict.text,
                        Type: this.userForm.Type,
                        diORx: this.userForm.diORx
                    }
                }
                generalGet(data).then((res) => {
                    if(res.msgCode == "Bad") {
                        that.$notify.error({
                            title: res.errorMsg
                        })
                    }else if(res.msgCode == "OK"){
                       that.$notify.success({
                            title: '添加成功'
                        }) ;
                        that.userForm = {
                            Account: '',
                            Password: '',
                            RealName: '',
                            DistrictCode: '',
                            DistrictName: '',
                            Type: "2",
                            diORx: "0",
                        }
                    }
                    console.log(res)
                })
            }
        }
    },
    mounted() {
        this.getDistrict();
    }
}
</script>

<style scoped>
.el-dropdown-link, .el-dropdown {
        width: 24rem;
    }
    .el-dropdown-link {
        cursor: pointer;
        color: #409EFF;
        overflow: hidden;
    }

    .el-icon-arrow-down {
        font-size: 12px;
    }
    .el-dropdown-menu {
        height: 50vh;
        overflow: scroll;
    }
    ::-webkit-scrollbar{
        display:none;
    }
</style>