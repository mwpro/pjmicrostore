module.exports = {
  devServer: {
    proxy: {
      '/api': {
        // target: 'http://localhost:64646', // visual studio
        target: 'http://localhost:9090', // docker-compose
        changeOrigin: true,
      },
    },
  },
};
