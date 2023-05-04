const { createProxyMiddleware } = require('http-proxy-middleware');

const context = [
    '/VendingMachine',
];

module.exports = function (app) {
    const appProxy = createProxyMiddleware(context, {
        target: 'http://localhost:5168',
        secure: false,
        changeOrigin: true,
    });

    app.use(appProxy);
};
