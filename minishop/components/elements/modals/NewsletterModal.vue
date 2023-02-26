<template>
    <div class="container newsletter-popup-container" id="newsletter-popup-form">
        <div class="row justify-content-center">
            <div class="col-12">
                <div class="row no-gutters bg-white newsletter-popup-content">
                    <div class="col-xl-3-5col col-lg-7 banner-content-wrap">
                        <div class="banner-content text-center">
                            <img
                                src="~/static/images/popup/newsletter/logo.png"
                                class="logo bg-white"
                                alt="logo"
                                width="60"
                                height="15"
                            />
                            <h2 class="banner-title">
                                get
                                <span>
                                    25
                                    <span class="font-weight-normal">%</span>
                                </span> off
                            </h2>
                            <p>Subscribe to the Molla eCommerce newsletter to receive timely updates from your favorite products.</p>
                            <form action="#">
                                <div class="input-group input-group-round">
                                    <input
                                        type="email"
                                        class="form-control form-control-white"
                                        placeholder="Your Email Address"
                                        aria-label="Email Adress"
                                        required
                                    />
                                    <div class="input-group-append">
                                        <button class="btn" type="submit">
                                            <span>go</span>
                                        </button>
                                    </div>
                                </div>
                            </form>
                            <div class="custom-control custom-checkbox mb-3">
                                <input
                                    type="checkbox"
                                    class="custom-control-input"
                                    id="register-policy-2"
                                    @change="changeCheck"
                                />
                                <label
                                    class="custom-control-label"
                                    for="register-policy-2"
                                >Do not show this popup again</label>
                            </div>
                        </div>
                    </div>
                    <div class="col-xl-2-5col col-lg-5">
                        <img
                            src="~/static/images/popup/newsletter/img-1.jpg"
                            width="396"
                            height="420"
                            class="newsletter-img"
                            alt="newsletter"
                        />
                    </div>
                </div>
            </div>
        </div>
        <button title="Close (Esc)" type="button" class="mfp-close" @click="closeModal">
            <span>×</span>
        </button>
    </div>
</template>
<script>
import { mapMutations } from 'vuex';
export default {
    data: function() {
        return {
            checkState: false
        };
    },
    watch: {
        $route: function() {
            this.$emit('close');
        }
    },
    methods: {
        ...mapMutations('demo', { hideNewsletter: 'HIDE_NEWSLETTER' }),
        changeCheck: function() {
            this.checkState = !this.checkState;
        },
        closeModal: function() {
            if (this.checkState) {
                this.hideNewsletter();
            }

            this.$emit('close');
        }
    }
};
</script>