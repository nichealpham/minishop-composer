export default {
    head: {
        titleTemplate: 'Mini e-commerce for Sandrasoft',
        title: 'Mini e-commerce for Sandrasoft',
        meta: [
            { charset: 'utf-8' },
            {
                name: 'viewport',
                content: 'width=device-width, initial-scale=1'
            },
            {
                hid: 'description',
                name: 'description',
                content:
                    'Mini Shop - Minimal and functional e-commerce platform for SME'
            },
            {
                name: 'author',
                content: 'd-themes'
            },
            {
                name: 'keywords',
                content: 'Sandrasoft mini shop e-commerce'
            },
            {
                name: 'app-mobile-web-app-title',
                content: 'Mini Shop'
            },
            {
                name: 'application-name',
                content: 'Mini Shop e-commerce'
            },
            {
                name: 'msapplication-TileColor',
                content: '#cc9966'
            },
            {
                name: 'msapplication-config',
                content: '/images/icons/browserconfig.xml'
            }
        ],
        link: [
            {
                rel: 'dns-prefetch',
                href: '//fonts.googleapis.com'
            },
            {
                rel: 'icon',
                type: 'image/png',
                sizes: '32x32',
                href: './images/icons/favicon-32x32.png'
            },
            {
                rel: 'icon',
                type: 'image/png',
                sizes: '16x16',
                href: './images/icons/favicon-16x16.png'
            },
            {
                rel: 'shortcut icon',
                href: './images/icons/favicon.ico'
            },
            {
                rel: 'apple-touch-icon',
                sizes: '180x180',
                href: './images/icons/apple-touch-icon.png'
            },
            {
                rel: 'manifest',
                href: './images/icons/site.webmanifest'
            },
            {
                rel: 'mask-icon',
                color: '#666666',
                href: './images/icons/safari-pinned-tab.svg'
            },
            {
                rel: 'stylesheet',
                href:
                    'https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700%7COpen Sans:300,400,500,600,700%7CRoboto:100,300,400,500,700,900',
                defer: true
            }
        ],
        script: [
            {
                hid: 'gtm',
                children: `(function(w, d, s, l, i) {
                    w[l] = w[l] || [];
                    w[l].push({
                      "gtm.start": new Date().getTime(),
                      event: "gtm.js",
                    });
                    var f = d.getElementsByTagName(s)[0],
                      j = d.createElement(s),
                      dl = l != "dataLayer" ? "&l=" + l : "";
                    j.async = true;
                    j.src = "https://www.googletagmanager.com/gtm.js?id=" + i + dl;
                    f.parentNode.insertBefore(j, f);
                  })(window, document, "script", "dataLayer", "GTM-K45RFW9");`,
                type: 'text/javascript'
            }
        ]
    },
    css: [
        '~/static/vendor/line-awesome/css/line-awesome.min.css',
        'swiper/dist/css/swiper.css',
        '~/static/css/fonts-molla.min.css',
        '~/static/css/bootstrap.min.css',
        '~/assets/scss/style.scss'
    ],

    plugins: [
        { src: '~/plugins/swiper.js', ssr: false },
        { src: '~/plugins/localStorage.js', ssr: false },
        { src: '~/plugins/modal.js', ssr: false },
        { src: '~/plugins/axios.js', ssr: false },
        { src: '~/plugins/lazyLoad.js', ssr: false },
        { src: '~/plugins/toastify.js', ssr: false },
        { src: '~/plugins/nouislider.js', ssr: false },
        { src: '~/plugins/constants.js', ssr: false }
    ],

    modules: ['@nuxtjs/axios'],

    router: {
        base: '/',
        linkActiveClass: 'link-active',
        linkExactActiveClass: 'active'
    },

    generate: {
        subFolders: false,
        fallback: '404.html'
    },

    ssr: false,

    server: {
        port: 4000,
        host: 'localhost'
    },

    target: 'static',
    env: {
        NUXT_APP_CRM_URL: process.env.NUXT_APP_CRM_URL,
        NUXT_APP_API_URL: process.env.NUXT_APP_API_URL,
        NUXT_APP_USER_OBJECT: process.env.NUXT_APP_USER_OBJECT,
        NUXT_APP_TOKEN_NAME: process.env.NUXT_APP_TOKEN_NAME,
        NUXT_APP_DEFAULT_LANGUAGE: process.env.NUXT_APP_DEFAULT_LANGUAGE
    }
};
