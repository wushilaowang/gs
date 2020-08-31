import requset from './request'

//登陆
export function login(data) {
    return requset({
        url: '/api/Oblogin',
        params: {
            account: data.account,
            password: data.password,
            districtCode: data.districtCode,//行政区划分码(前六位)
            entrance: data.entrance,//全国测评,省级测评
            diORx: data.diORx//地级(0),县级(1)
        }
    })
}