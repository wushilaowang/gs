const path = require('path');
function resolve(dir) {
    return path.join(__dirname, dir);
}



module.exports = {
    publicPath: './',
    chainWebpack: (config) => {
        config.resolve.alias
            .set('@', resolve('src'))
    },
    devServer: {
        open: false,
        host: '0.0.0.0',
        port: 8082,
        https: false,
        hotOnly: false,
        proxy: {
            '/api': {
                target: 'http://211.158.66.55/wmgz',//'https://wmcscp.gsinfo.cn/localwmcs',////'https://wmcscp.gsinfo.cn',// 你接口的域名
                secure: true, // 如果是https接口，需要配置这个参数为true
                changeOrigin: true, // 如果接口跨域，需要进行这个参数配置为true
                pathRewrite: {
                    '^/api': ''
                }
            }
        }
    }
}