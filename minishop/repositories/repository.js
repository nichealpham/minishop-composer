import axios from 'axios';

const baseDomain = 'https://d-themes.com/vue/molla/server';

export const customHeaders = {
    'Content-Type': 'application/json',
    Accept: 'application/json',
};

export const baseUrl = `${baseDomain}`;

export default axios.create( {
    baseUrl,
    headers: customHeaders
} );