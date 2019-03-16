module.exports = {
    devServer: {
      proxy: {
        '/api': {
          target: 'http://localhost:53606',
          changeOrigin: true,
        },
      },
    },
  };
  