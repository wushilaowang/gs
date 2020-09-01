<template>
    <div>
        <el-tree ref="tree" node-key="id" :default-expanded-keys="defaultExpandedKeys" :default-checked-keys="defaultCheckedKeys"  :data="districtList" show-checkbox :props="props">

        </el-tree>
        <el-button type="success" size="small" @click="getCheckedNodes">确定</el-button>
    </div>
</template>

<script>
import {generalGet} from '../network/general'
export default {
    name: 'districtTree',
    props: {
        currentUser: {
            type: Object,
            default: {}
        }
    },
    data() {
        return {
            originDistrictList: [],
            districtList: [],
            props: {
                label: 'title',
                children: 'children'
            },
            defaultCheckedKeys: [],
            defaultExpandedKeys: [1, 2]
        }
    },
    methods: {
        //获取地域数据
        getDistrict() {
            generalGet({url: '/api/ShowAlldistrict'}).then(res => {
                this.originDistrictList = res.data;
                console.log(res)
                let dataList = res.data;
                res.data.push({})
                let currentCity = '';
                let districtList = [];
                let childList = [];
                let index = 10000;
                dataList.map(item => {
                    this.currentUser.districtName.split(',').map(item2 => {
                        if(item2 == item.districtName) {
                            this.defaultCheckedKeys.push(item.districtId)
                        }
                    })
                    if(currentCity != item.owenerCityName){
                        if(currentCity != ''){
                            districtList.push({id: index, title: currentCity, children: childList});
                            childList = [];
                        }
                        childList.push({id: item.districtId, title: item.districtName, code: item.districtCode, parrentId: index + 1});
                        currentCity = item.owenerCityName;
                        index ++;
                    }else {
                        childList.push({id: item.districtId, title: item.districtName, code: item.districtCode, parrentId: index})
                    }
                })
                this.districtList = districtList
                console.log(this.districtList)
            })
        },
        //保存选择的地域
        getCheckedNodes() {
            //获取勾选地域id数组
            let checkedArr = this.$refs.tree.getCheckedKeys().filter(item => {
                console.log(item)
                return item < 1000
            })
            console.log(this.$refs.tree.getCheckedKeys());
            let districtCodeArr1 = [];
            let districtNameArr1 = [];
            this.originDistrictList.map(item => {
                checkedArr.map(item2 => {
                    if(item.districtId == item2) {
                        districtCodeArr1.push(item.districtCode);
                        districtNameArr1.push(item.districtName)
                    }
                })
            });
            //districtCode字符串
            let districtCodeArr = districtCodeArr1.map(item => {
                return item
            }).join(',')
            //districtName字符串
            let districtNameArr = districtNameArr1.map(item => {
                return item
            }).join(',')
            console.log(this.currentUser)
            let data = {
                url: '/api/SetUser',
                params: {
                    Account: this.currentUser.account,
                    Password: this.currentUser.password,
                    RealName: this.currentUser.realName,
                    DistrictCode: districtCodeArr,
                    DistrictName: districtNameArr,
                    Type: this.currentUser.type,
                    entrance: this.$store.state.entrance,
                    AddOrUpd: 0
                }
            }
            let that = this;
            generalGet(data).then(res => {
                console.log(res)
                if(res.msgCode == 'OK') {
                    that.$notify({
                        type: 'success',
                        title: res.errorMsg
                    })
                }else if(res.msgCode != '成功') {
                    that.$notify.error({
                        title: res.errorMsg
                    })
                }
            });
            //
            this.$emit('reloadFatherUserTable')
        }
    },
    mounted() {
        this.getDistrict();
        console.log(this.currentUser.districtName)
    }
}
</script>

<style scoped>
</style>