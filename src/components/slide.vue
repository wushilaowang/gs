<template>
    <div class="box">
        <div class="photo-list">
            <ul>
                <li v-for="(item,index) in imageArr" :key="index"><img :src="item"/></li>
            </ul>
        </div>
        <div class="circle-list">
            <div v-for="(item,index) in imageArr" class="circle-item" :key="index"></div>
        </div>
    </div>
</template>

<script>
export default {
    name: 'slide',
    props: {
        imageArr: {
            type: Array,
            default: [require('../assets/00300035230_e092f172.jpg'), require('../assets/00300035230_e092f173.jpg'), require('../assets/00300035230_e092f174.jpg')],
        }
    }
}
</script>

<style scoped>
    li {
        list-style: none;
        margin: 0;
        padding: 0;
    }
    .box {
        position: relative;
    }
    .photo-list li{
        float: left;
    }
    .circle-list {
        display: flex;
        position: absolute;
    }
    .circle-item {
        width: 1rem;
        height: 1rem;
        border-radius: 50%;
        background-color: #a1a1a1;
        margin: .5rem .5rem;
    }
    .circle-item:hover {
        cursor: pointer;
        background-color: #fff;
    }
</style>