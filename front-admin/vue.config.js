module.exports = {
  devServer: {
    proxy: {
      '/api': {
        // target: 'http://localhost:63646', // visual studio
        target: 'http://localhost:9091', // docker-compose
        changeOrigin: true,
      },
    },
  },
};
