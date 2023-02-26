<template>
    <div :class="wrap ? wrap : 'product-countdown is-countdown'">
        <span class="countdown-row countdown-show4" v-if="format !== 'HMS' && labelsShort">
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > days ? '0' + days : days }}</span>
                <span class="countdown-period">Days</span>
            </span>
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > hours ? '0' + hours : hours }}</span>
                <span class="countdown-period">Hours</span>
            </span>
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > minutes ? '0' + minutes : minutes }}</span>
                <span class="countdown-period">Mins</span>
            </span>
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > seconds ? '0' + seconds : seconds }}</span>
                <span class="countdown-period">Secs</span>
            </span>
        </span>
        <span
            class="countdown-row countdown-show4"
            v-if="format !== 'HMS' && ! labelsShort && ! compact"
        >
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > days ? '0' + days : days }}</span>
                <span class="countdown-period">Days</span>
            </span>
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > hours ? '0' + hours : hours }}</span>
                <span class="countdown-period">Hours</span>
            </span>
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > minutes ? '0' + minutes : minutes }}</span>
                <span class="countdown-period">Minutes</span>
            </span>
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > seconds ? '0' + seconds : seconds }}</span>
                <span class="countdown-period">Seconds</span>
            </span>
        </span>
        <span
            class="countdown-row countdown-amount"
            v-if="format !== 'HMS' && ! labelsShort && compact"
        >{{ days + ' DAYS, ' + ( 10 > hours ? '0' : '' ) + hours + ': ' + ( 10 > minutes ? '0' : '' ) + minutes + ': ' + ( 10 > seconds ? '0' : '' ) + seconds }}</span>

        <span class="countdown-row countdown-show3" v-if="format === 'HMS' && ! labelsShort">
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > hours ? '0' + hours : hours }}</span>
                <span class="countdown-period">Hours</span>
            </span>
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > minutes ? '0' + minutes : minutes }}</span>
                <span class="countdown-period">Minutes</span>
            </span>
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > seconds ? '0' + seconds : seconds }}</span>
                <span class="countdown-period">Seconds</span>
            </span>
        </span>

        <span class="countdown-row countdown-show3" v-if="format === 'HMS' && labelsShort">
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > hours ? '0' + hours : hours }}</span>
                <span class="countdown-period">hrs</span>
            </span>
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > minutes ? '0' + minutes : minutes }}</span>
                <span class="countdown-period">mins</span>
            </span>
            <span class="countdown-section">
                <span class="countdown-amount">{{ 10 > seconds ? '0' + seconds : seconds }}</span>
                <span class="countdown-period">secs</span>
            </span>
        </span>
    </div>
</template>
<script>
export default {
    props: {
        compact: Boolean,
        format: String,
        elements: {
            type: Number,
            default: function() {
                return 4;
            }
        },
        labelsShort: Boolean,
        until: String,
        wrap: String
    },
    data: function() {
        return {
            cdId: null,
            days: null,
            hours: null,
            minutes: null,
            seconds: null,
            time: null
        };
    },
    created: function() {
        this.cdId = 'countd-down' + Math.ceil(Math.random() * 1000);
    },
    mounted: function() {
        this.time = this.until.substring(1, this.until.length - 1);
        switch (this.until[this.until.length - 1]) {
            case 'h':
                this.time = this.time * 3600;
                break;
            case 'm':
                this.time = this.time * 60;
                break;
            case 'd':
                this.time = this.time * 3600 * 24;
                break;
            default:
                break;
        }

        let until = new Date(this.until);
        let current = new Date();
        let time = (until - current) / 1000;

        if (this.until.substring(0, 1) === '+') time = this.time;

        this.days = Math.floor(time / (3600 * 24));
        this.hours = Math.floor((time % (3600 * 24)) / 3600);
        this.minutes = Math.floor((time % 3600) / 60);
        this.seconds = Math.floor(time % 60);

        setInterval(() => {
            let until = new Date(this.until);
            let current = new Date();
            let time = (until - current) / 1000;

            if (this.until.substring(0, 1) === '+') time = this.time;
            this.time--;

            this.days = Math.floor(time / (3600 * 24));
            this.hours = Math.floor((time % (3600 * 24)) / 3600);
            this.minutes = Math.floor((time % 3600) / 60);
            this.seconds = Math.floor(time % 60);
        }, 1000);
    }
};
</script>