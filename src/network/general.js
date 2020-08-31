import request from './request'

export function generalGet(data) {
    return request({
        method: 'GET',
        url: data.url,
        params: data.params
    })
}


let param = new URLSearchParams();
export function generalPost(data) {
    console.log(data)
    for(let i in data.params) {
        param.append(i, data.params[i])
    }
    return request({
        method: 'post',
        url: data.url,
        data: param
    })
}