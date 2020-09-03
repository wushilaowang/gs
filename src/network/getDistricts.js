import {generalGet} from './general'

export async function getDistrict() {
    let result = [];
    generalGet({url: '/api/ShowAlldistrict'}).then(res => {
        console.log(res)
        let dataList = res.data;
        res.data.push({})
        let currentCity = '';
        let districtList = [];
        let childList = [];
        let index = 10000;
        dataList.map(item => {
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
        result = dataList;
    })
    return result;
}