module.exports = {
  plugins: [
    require('postcss-import'),
    require('postcss-cssnext'),
    require('postcss-custom-media'),
    // require('cssnano')({
    //   //preset: 'default',
    //   discardComments: {removeAll: true},
    //   minifyFontValues: false,
    //   autoprefixer: false
    // })
  ]
}