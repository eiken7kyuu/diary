const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin')
const ForkTsCheckerWebpackPlugin = require('fork-ts-checker-webpack-plugin');
const VueLoaderPlugin = require('vue-loader/lib/plugin')

module.exports = {
  mode: 'development',
  entry: path.join(__dirname, './js', 'app.ts'),
  output: {
    path: path.resolve(__dirname, '../wwwroot/'),
    filename: 'js/bundle.js'
  },
  module: {
    rules: [
      // css
      {
        test: /\.(css|scss)$/,

        use: [
          MiniCssExtractPlugin.loader,
          'css-loader',

          {
            loader: 'sass-loader',
            options: {
              sourceMap: true,
              implementation: require('sass'),
              sassOptions: {
                fiber: require("fibers"),
              },
            }
          }
        ]
      },

      // .vue
      {
        test: /\.vue$/,
        loader: "vue-loader"
      },

      // js, ts, tsx
      {
        test: /\.(js|ts|tsx)$/,
        exclude: /(node_modules)/,

        use: [
          {
            loader: 'babel-loader',

            options: {
              presets: [
                ["@babel/preset-env", {
                  'useBuiltIns': 'usage',
                  'corejs': 3
                }],
              ]
            }
          },
          {
            loader: 'ts-loader',
            options: {
              transpileOnly: true
            }
          },
        ]
      }
    ]
  },
  plugins: [
    new VueLoaderPlugin(),

    new MiniCssExtractPlugin({
      filename: 'css/site.css'
    }),

    new ForkTsCheckerWebpackPlugin({
      typescript: {
        configFile: path.join(__dirname, './tsconfig.json')
      }
    }),
  ],

  cache: true,
  resolve: {
    alias: {
      'vue$': 'vue/dist/vue.esm.js'
    }
  }
};