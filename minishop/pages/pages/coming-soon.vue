<template>
    <div class="soon">
        <div class="container">
            <div class="row">
                <div class="col-md-9 col-lg-8">
                    <div class="soon-content text-center">
                        <div class="soon-content-wrapper">
                            <img
                                src="~/static/images/logo-icon.png"
                                width="26"
                                height="25"
                                alt="Logo"
                                class="soon-logo mx-auto bg-white"
                            />
                            <h1 class="soon-title">Coming Soon</h1>

                            <count-down
                                until="2021-07-20"
                                format="DHMS"
                                wrap="coming-countdown countdown-separator"
                            ></count-down>

                            <hr class="mt-2 mb-3 mt-md-3" />
                            <p>
                                We are currently working on an awesome new site. Stay tuned for more information.
                                Subscribe to our newsletter to stay updated on our progress.
                            </p>
                            <form action="#">
                                <div class="input-group mb-5">
                                    <input
                                        type="email"
                                        class="form-control bg-transparent"
                                        placeholder="Enter your Email Address"
                                        required
                                    />
                                    <div class="input-group-append">
                                        <button class="btn btn-outline-primary-2" type="submit">
                                            <span>SUBSCRIBE</span>
                                            <i class="icon-long-arrow-right"></i>
                                        </button>
                                    </div>
                                </div>
                            </form>
                            <div class="social-icons justify-content-center mb-0">
                                <a href="#" class="social-icon" target="_blank" title="Facebook">
                                    <i class="icon-facebook-f"></i>
                                </a>
                                <a href="#" class="social-icon" target="_blank" title="Twitter">
                                    <i class="icon-twitter"></i>
                                </a>
                                <a href="#" class="social-icon" target="_blank" title="Instagram">
                                    <i class="icon-instagram"></i>
                                </a>
                                <a href="#" class="social-icon" target="_blank" title="Youtube">
                                    <i class="icon-youtube"></i>
                                </a>
                                <a href="#" class="social-icon" target="_blank" title="Pinterest">
                                    <i class="icon-pinterest"></i>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="soon-bg bg-image" v-lazy:background-image="'./images/backgrounds/soon-bg.jpg'"></div>
    </div>
</template>
<script>
import CountDown from '~/components/elements/CountDown';
export default {
    components: {
        CountDown
    },
    layout: 'empty'
};
</script>