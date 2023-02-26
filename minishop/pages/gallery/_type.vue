<template>
    <main class="main">
        <div class="page-content">
            <div
                class="container container_padding"
                style="background:white;border-radius:6px;"
            >
                <div class="heading heading-center">
                    <h2 class="title">DANH SÁCH ẢNH</h2>
                </div>
                <div class="tab-content">
                    <div class="tab-pane fade show active p-0">
                        <div class="products">
                            <div
                                class="container skeleton-body horizontal"
                                v-if="!loading"
                            >
                                <div
                                    v-for="n in 4"
                                    :key="n"
                                    class="skel-pro col-6 col-md-3 col-lg-3 loaded"
                                    style="float:left;"
                                ></div>
                            </div>
                            <div class="row justify-content-center" v-else>
                                <!-- <div
                                    class="col-6 col-md-4 col-lg-3"
                                    v-for="(product, index) in ImageData"
                                    :key="index"
                                >
                                    <p>{{ product.attachmentUrl }}</p>
                                </div> -->
                                <div
                                    class="col-6 col-md-4 col-lg-3"
                                    v-for="(image, index) in ImageData"
                                    :key="index"
                                >
                                    <img
                                        :src="image.attachmentUrl"
                                        alt="image"
                                        class="image galery_item"
                                        @click.prevent="openLightBox(index)"
                                    />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <light-box
            v-if="IsShowLightBox"
            ref="lightbox"
            class="light-box"
            :media="lightBoxMedia"
            :show-caption="true"
            :onOpened="openLightBoxTest()"
        >
        </light-box>
    </main>
</template>

<script>
import { $httpClient } from '~/plugins/constants';
import LightBox from 'vue-image-lightbox';
import 'vue-image-lightbox/dist/vue-image-lightbox.min.css';
import Buttons from '../elements/buttons.vue';
export default {
    components: {
        LightBox,
        Buttons
    },
    data: function() {
        return {
            loading: false,
            ImageData: [],
            result: {},
            currentIndex: 0,
            IsShowLightBox: false
        };
    },
    created() {
        this.getDataImage();
    },
    computed: {
        lightBoxMedia: function() {
            let arrImage = [];
            for (let index = 0; index < this.ImageData.length; index++) {
                const element = {
                    thumb: this.ImageData[index].attachmentUrl,
                    src: this.ImageData[index].attachmentUrl
                };
                arrImage.push(element);
            }
            return arrImage;
        }
    },
    methods: {
        async getDataImage() {
            console.log(`/Public/Companies/${this.$companyName}/ImageGallery`);

            const result = await $httpClient.get(
                `/Public/Companies/${this.$companyName}/ImageGallery`,
                {},
                { page: 1, limit: 100 }
            );
            if (result) {
                console.log(result);
                this.loading = true;
                this.ImageData = result.items;
            }
        },
        async openLightBox(index) {
            this.IsShowLightBox = true;
            await this.sleep(50);
            this.$refs.lightbox.showImage(index);
            this.currentIndex = index;
        },
        async openLightBoxTest() {
            await this.sleep(50);
            const content = document
                .getElementsByClassName('vue-lb-figure')
                .item(0);
            content.addEventListener('click', () => {
                window.location.href = `/product/default/${
                    this.ImageData[this.currentIndex].assetID
                }`;
            });
        }
    }
};
</script>
<style lang="scss" scoped>
.galery_item {
    cursor: pointer;
    transition: all 0.3s;
    object-fit: cover;
    height: 100%;
    width: 100%;
    // padding: 5px;
    padding-bottom: 10% !important;
    padding-right: 2%;
}
.galery_item:hover {
    transform: scale(1.1, 1.1);
}
</style>
