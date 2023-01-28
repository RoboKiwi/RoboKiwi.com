module.exports = {
    plugins: [
        require('postcss-preset-env'),
        require('postcss-import'),
        require('postcss-custom-media'),
        require('autoprefixer'),
        require('postcss-nested'),
        require('postcss-csso')
  ]
}