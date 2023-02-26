import Vue from 'vue';

Vue.mixin({
    computed: {
        $companyName() {
            var { query } = this.$route;
            var result = query.cname;
            if (result) {
                localStorage.setItem('COMPANY_NAME_UUID', result);
            } else {
                result = localStorage.getItem('COMPANY_NAME_UUID');
                if (!result) {
                    result = 'demostore';
                    // result = 'eshop408';
                    localStorage.setItem('COMPANY_NAME_UUID', result);
                }
            }
            return result;
        },
        $httpClient() {
            return $httpClient;
        },
        $crmUrlHome() {
            return `${process.env.NUXT_APP_CRM_URL}`;
        },
        $crmUrl() {
            var current = window.location.href;
            return `${process.env.NUXT_APP_CRM_URL}/episode?redirect=${current}`;
        },
        $crmLoginUrl() {
            var current = window.location.href;
            return `${process.env.NUXT_APP_CRM_URL}/login?redirect=${current}`;
        },
        $crmLogoutUrl() {
            var current = window.location.href;
            return `${process.env.NUXT_APP_CRM_URL}/logout?redirect=${current}`;
        },
        $crmRegisterUrl() {
            var current = window.location.href;
            return `${process.env.NUXT_APP_CRM_URL}/register?redirect=${current}`;
        }
    },
    methods: {
        openNewTab(url) {
            window.open(url);
        },
        openCrmLoginUrl() {
            // window.location.href = this.$crmLoginUrl;
            // this.openNewTab(this.$crmLoginUrl);
            var current = window.location.href;
            // window.location.href = `/account?redirect=${current}`;
            this.$store.commit('auth/SET_IS_REGISTER', false);
            this.$store.commit('auth/SET_REDIRECT_URL', current);
            window.location.href = `/account`;
        },
        openCrmRegisterUrl() {
            // window.location.href = this.$crmRegisterUrl;
            // this.openNewTab(this.$crmRegisterUrl);
            var current = window.location.href;
            // window.location.href = `/account?register=true&redirect=${current}`;
            this.$store.commit('auth/SET_IS_REGISTER', true);
            this.$store.commit('auth/SET_REDIRECT_URL', current);
            window.location.href = `/account`;
        },
        callPhone(phone) {
            window.open(`tel:${phone}`);
        },
        getCompanyName() {
            return this.$route.params.companyName;
        },
        sleep(milliseconds) {
            return sleep(milliseconds);
        },
        formatPrice(val, isVND = true, forceMinus = false) {
            if (val == 0) return isVND ? '0.000 VND' : 'Miễn phí';
            if (!val || isNaN(val)) return '';

            // if val is actually < 0 => forceMinus = true
            if (val < 0) {
                return formatPrice(Math.abs(val), isVND, true);
            }

            const valStr = typeof val === 'string' ? val : val.toString();
            const valDec = valStr.split('.')[0];

            var result =
                valDec.replace(/./g, function(c, i, a) {
                    return i > 0 && c !== '.' && (a.length - i) % 3 === 0
                        ? '.' + c
                        : c;
                }) + (isVND ? ' VND' : '');

            return `${forceMinus ? '- ' : ''}` + result;
        }
    }
});

export const sleep = (milliseconds = 100) => {
    return new Promise(resolve => {
        var timeout = setTimeout(() => {
            clearTimeout(timeout);
            resolve();
        }, milliseconds);
    });
};

export function setLocalStorage(lname, lvalue) {
    localStorage.setItem(lname, lvalue);
    return true;
}

export function getLocalStorage(lname) {
    const stringData = localStorage.getItem(lname);
    if (!stringData) {
        return null;
    }
    return stringData;
}

export function parseLocalStorage(lname) {
    const stringData = getLocalStorage(lname);
    if (!stringData) {
        return null;
    }
    return JSON.parse(stringData);
}

export const TargetItemType = {
    Service: 1,
    Asset: 2,
    Episode: 3
};

export default ({ app }, inject) => {
    inject('constants', {
        companyName() {
            var { router } = app;
            var { currentRoute } = router;
            var { params } = currentRoute;
            return params.companyName || 'demostore';
        }
    });
};

import axios from 'axios';

export class HttpClient {
    constructor(baseUrl) {
        this.axiosInstance = axios.create({
            baseURL: baseUrl,
            timeout: 300000
        });
    }

    getOptions(header = {}) {
        var options = {
            headers: buildHeaders(header)
        };
        return options;
    }

    handleError(err) {
        var { response } = err;
        // if API error
        if (response) {
            var { status } = response;
            // If error code == 401 => Token expired or invalid
            if (status == 401) {
                // Remove all localstorage
                removeLocalStorage(process.env.NUXT_APP_TOKEN_NAME);
                removeLocalStorage(process.env.NUXT_APP_USER_OBJECT);
            }
        }
        let apiResponse = new ApiResponse(err, true);
        let resultError = apiResponse.formatResponse().getResult();
        return resultError;
    }

    get(url, query = {}, headers = {}) {
        let options = this.getOptions(headers);
        url = buildUrl(url, query);

        return new Promise(resolve => {
            this.axiosInstance
                .get(url, options)
                .then(res => resolve(res.data))
                .catch(err => resolve(this.handleError(err)));
        });
    }

    getFile(
        url,
        query = {},
        headers = {},
        contentType = 'plain/text',
        filename = 'DownloadedFile'
    ) {
        let options = this.getOptions(headers);
        options['Content-Type'] = contentType;
        options['responseType'] = 'blob';
        url = buildUrl(url, query);

        return new Promise(resolve => {
            this.axiosInstance
                .get(url, options)
                .then(res => {
                    var { data } = res;
                    const url = window.URL.createObjectURL(
                        new Blob([data], {
                            type: contentType
                        })
                    );
                    const link = document.createElement('a');
                    link.href = url;
                    link.setAttribute('download', filename);
                    document.body.appendChild(link);
                    link.click();
                    document.body.removeChild(link);
                    resolve(true);
                })
                .catch(err => resolve(this.handleError(err)));
        });
    }

    post(url, query = {}, body = {}, headers = {}) {
        let options = this.getOptions(headers);
        url = buildUrl(url, query);

        return new Promise(resolve => {
            this.axiosInstance
                .post(url, body, options)
                .then(res => resolve(res.data))
                .catch(err => resolve(this.handleError(err)));
        });
    }

    put(url, query = {}, body = {}, headers = {}) {
        let options = this.getOptions(headers);
        url = buildUrl(url, query);

        return new Promise(resolve => {
            this.axiosInstance
                .put(url, body, options)
                .then(res => resolve(res.data))
                .catch(err => resolve(this.handleError(err)));
        });
    }

    delete(url, query = {}, headers = {}) {
        let options = this.getOptions(headers);
        url = buildUrl(url, query);

        return new Promise(resolve => {
            this.axiosInstance
                .delete(url, options)
                .then(res => resolve(res.data))
                .catch(err => resolve(this.handleError(err)));
        });
    }

    postFile(
        url,
        query = {},
        file,
        body = {},
        headers = {},
        onUploadProgress = null
    ) {
        let formData = new FormData();
        formData.append('file-upload', file);

        for (let key in body) {
            formData.append(key, body[key]);
        }

        let options = this.getOptions(headers);
        if (onUploadProgress) {
            options.onUploadProgress = onUploadProgress;
        }

        url = buildUrl(url, query);

        return new Promise(resolve => {
            this.axiosInstance
                .post(url, formData, options)
                .then(res => resolve(res.data))
                .catch(err => resolve(this.handleError(err)));
        });
    }
}

// handler for axios
export class ApiResponse {
    StatusAPI = {
        //
        100: 'Continue',
        101: 'Switching Protocol',
        102: 'Processing',
        103: 'Early Hints',
        //
        200: 'OK',
        201: 'Created',
        202: 'Accept',
        203: 'Non-Authoritative Information',
        204: 'No content',
        //
        300: 'Multiple choices',
        301: 'Moved Permanently',
        302: 'Found',
        303: 'See Other',
        304: 'Not Modified',
        307: 'Temporary Redirect',
        //
        400: 'Bad request! Or some inputs are missing',
        401: 'Unauthorized! You are not allowed to access!',
        403: 'Forbidden',
        404: 'Not found! Or the url is not correct!',
        405: 'Method not allowed!',
        406: 'Not acceptable!',
        412: 'Precondition failed',
        415: 'Unsupported media type',
        //
        500: 'Internal Server Error! Or something makes it broken',
        501: 'Not Implemented',
        0: 'Unknown status code'
    };

    statusCode;
    data = null;
    header = '';
    message = '';
    error;
    axiosInstance;

    constructor(axiosInstance, error = false) {
        this.axiosInstance = axiosInstance;
        this.error = error;
    }
    // format response
    formatResponse() {
        if (this.error) {
            if (this.axiosInstance.response) {
                // The request was made and the server responded with a status code
                // that falls out of the range of 2xx
                this.statusCode = this.axiosInstance.response.status;
                this.data = this.axiosInstance.response.data;
                this.header = this.axiosInstance.response.headers;
                // format message
                // this.data: from Back end return
                if (this.data != null) {
                    if (this.data.errorMessage) {
                        this.message = convertLongWordToEasyWord(
                            this.data.errorMessage,
                            ''
                        );
                    } else {
                        this.message = this.data.errorName
                            ? convertLongWordToEasyWord(this.data.errorName, '')
                            : this.StatusAPI[this.statusCode];
                    }
                } else {
                    this.message = this.StatusAPI[this.statusCode]
                        ? this.StatusAPI[this.statusCode]
                        : 'Something went wrong!';
                }
            } else if (this.axiosInstance.request) {
                // the request was made but no response was received
                this.message =
                    'Not response from server! Something went wrong!';
                this.statusCode = 0;
            } else {
                // Something happened in setting up the request that triggered an Error
                this.message = this.axiosInstance.message;
                this.statusCode = 0;
            }
        }
        return this;
    }

    getResult() {
        return {
            error: this.axiosInstance.toString(),
            message: this.message,
            statusCode: this.statusCode,
            header: this.header
        };
    }
}

export const convertLongWordToEasyWord = (stringInput = '', result) => {
    let string = stringInput;
    let resultString = result;

    for (let i = 0; i < string.length; i++) {
        let char = string.charAt(i);
        if (char == ' ') {
            i = i + 1;
            char = string.charAt(i).toUpperCase(); // get next char
        }
        if (char == char.toUpperCase()) {
            resultString =
                i == 0 && resultString == '' ? char : `${resultString} ${char}`;
        } else {
            if (i == 0 && resultString == '') {
                resultString = char.toUpperCase();
            } else {
                resultString += `${char}`;
            }
        }
        if (i + 1 == string.length) {
            return resultString;
        }
        let nextString = string.slice(i + 1, string.length);
        return convertLongWordToEasyWord(nextString, resultString);
    }
    return result;
};

export function buildUrl(url, query) {
    if (!query || query === {} || Array.isArray(query)) {
        return url;
    }
    for (const keyname in query) {
        if (typeof query[keyname] == 'object') {
            continue;
        }
        const param = `{${keyname}}`;
        while (url.includes(param)) {
            url = url.replace(param, query[keyname]);
        }
    }
    const queries = [];
    for (const keyname in query) {
        if (typeof query[keyname] !== 'object') {
            queries.push(`${keyname}=${query[keyname]}`);
            continue;
        }
        if (Array.isArray(query[keyname])) {
            queries.push(`${keyname}=${query[keyname].join(',')}`);
        } else {
            queries.push(`${keyname}=${JSON.stringify(query[keyname])}`);
        }
    }
    return `${url}?${queries.join('&')}`;
}

export function getDefaultRequestHeaders() {
    return {
        page: 1,
        limit: 10,
        'Content-Type': 'application/json'
    };
}

export function buildHeaders(headers) {
    let requestHeaders = getDefaultRequestHeaders();

    if (!headers) {
        return requestHeaders;
    }
    if (Array.isArray(headers)) {
        return requestHeaders;
    }
    for (let keyname in headers) {
        requestHeaders[keyname.toLowerCase()] = headers[keyname];
    }

    return requestHeaders;
}

export function removeLocalStorage(lname) {
    localStorage.removeItem(lname);
    return true;
}

export const $httpClient = new HttpClient(process.env.NUXT_APP_API_URL);
