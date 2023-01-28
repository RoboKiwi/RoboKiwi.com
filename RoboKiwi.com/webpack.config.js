const path = require('path');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");

module.exports = {
    output: {
        path: path.resolve(__dirname, 'wwwroot'),
    },
    plugins: [new MiniCssExtractPlugin()],
    module: {
        rules: [
            {
                test: /\.css$/i,
                use: [
                    MiniCssExtractPlugin.loader,
                    //"style-loader",
                    { loader: 'css-loader', options: { importLoaders: 1 } },
                    "postcss-loader"
                ],
            },
        ],
    },
};