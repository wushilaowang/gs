<template>
    <div>
        <el-table :data="userAuthorityTable">
            <el-table-column label="类型" prop="typeCode"></el-table-column>
            <el-table-column label="名称" prop="typeName"></el-table-column>

        </el-table>
    </div>
</template>

<script>
import {generalGet} from '../../network/general'
export default {
    name: 'userAuthority',
    data() {
        return {
            userAuthorityTable: []
        }
    },
    methods: {
        loadUserAuthority() {
            let that = this;
            let data = {
                url: '/api/ShowAllUserType'
            }
            generalGet(data).then((res) => {
                console.log(res);
                that.userAuthorityTable = res.data
            })
        }
    },
    mounted() {
        this.loadUserAuthority();
    },
}
</script>

<style scoped>

</style>