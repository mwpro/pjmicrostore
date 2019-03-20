module.exports = {
    devServer: {
      proxy: {
        '/api/products': {
          target: 'http://localhost:53606',
          changeOrigin: true,
        },
        '/api/categories': {
            target: 'http://localhost:53606',
            changeOrigin: true,
        },
        '/api/cart': {
            target: 'http://localhost:64642',
            changeOrigin: true,
        },        
        '/api/orders': {
            target: 'http://localhost:56037',
            changeOrigin: true,
        },
        '/api/payments': {
            target: 'http://localhost:62678',
            changeOrigin: true,
        }
      },
    },
  };
  