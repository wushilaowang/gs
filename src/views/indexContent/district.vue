<template>
    <div class="">
        <el-table :data="districtTable">
            <el-table-column label="地区" prop="districtName"></el-table-column>
            <el-table-column label="所属地区" prop="owenerCityName"></el-table-column>
            <el-table-column label="行政区划代码" prop="districtCode"></el-table-column>

        </el-table>

    </div>
</template>

<script>
import {generalGet} from '../../network/general'
export default {
    name: 'district',
    data() {
        return {
            districtTable: []
        }
    },
    methods: {
        loadDistrict() {
            let that = this;
            let data = {
                url: '/api/ShowAlldistrict'
            }
            generalGet(data).then((res) => {
                console.log(res);
                that.districtTable = res.data
            })
        }
    },
    mounted() {
        this.loadDistrict();
    },
}
</script>

<style scoped>

</style>