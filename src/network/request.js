import originAxios from 'axios'

export default function request(option) {
    return new Promise((resolve, reject) => {
        const instance = originAxios.create({
            baseURL: "/api",
            timeout: 20000
        })


        instance.interceptors.request.use(config => {
            return config;
        }, err => {
            return err;
        })

        instance.interceptors.response.use(response => {
            return response;
        }, err => {
            console.log(err);
            switch(err.response.status) {
                case 500: 
                    err.message = "请求失败500";
                    break;
            }
            return err;
        })


        return instance(option).then(res => {
            resolve(res.data);
        }).catch(err => {
            reject(err);
        })
    })
}