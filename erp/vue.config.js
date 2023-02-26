const path = require("path");

function resolveSrc(_path) {
  return path.join(__dirname, _path);
}
// vue.config.js
module.exports = {
  lintOnSave: true,
  configureWebpack: {
    // Set up all the aliases we use in our app.
    resolve: {
      alias: {
        assets: resolveSrc("src/assets"),
      },
    },
  },
  css: {
    // Enable CSS source maps.
    sourceMap: process.env.NODE_ENV !== "production",
  },
  // Use Https to test Facebook Login
  devServer: {
    open: process.platform === "darwin",
    host: "0.0.0.0",
    port: 8080,
    // https: true,
    hotOnly: false,
  },
  pwa: {
    name: "Sandrasoft",
    themeColor: "#605bff",
    msTileColor: "#605bff",
    appleMobileWebAppCapable: "yes",
    appleMobileWebAppStatusBarStyle: "black",
  },
};
