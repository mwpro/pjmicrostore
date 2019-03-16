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
            target: 'https://localhost:44350',
            changeOrigin: true,
        },        
        '/api/orders': {
            target: 'https://localhost:44377',
            changeOrigin: true,
        },
      },
    },
  };
  