module.exports = {
  devServer: {
    proxy: {
      '/api': {
        target: 'http://localhost:64646',
        changeOrigin: true,
      },
    },
  },
};
