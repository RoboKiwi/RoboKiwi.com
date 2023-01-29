module.exports = {
  plugins: [
    require('postcss-import')({'path': './src/css'}),
    require('postcss-cssnext'),
    require('postcss-custom-media'),
    require('cssnano')({
      discardComments: {removeAll: true},
      minifyFontValues: false,
      autoprefixer: false
    }),
  ]
}