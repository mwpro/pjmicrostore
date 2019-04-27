module.exports = {
  devServer: {
    proxy: {
      '/': {
        target: 'http://localhost:64646',
        changeOrigin: true,
      },
    },
  },
};
