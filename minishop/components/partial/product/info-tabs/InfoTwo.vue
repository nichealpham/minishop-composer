<template>
    <div class="product-details-tab product-details-extended">
        <tabs class="nav-pills justify-content-center" :data="tabsData"></tabs>
        <div class="tab-content border-bottom-0">
            <div class="tab-pane fade show active" id="tab1">
                <div class="product-desc-content">
                    <div
                        class="product-desc-row bg-image"
                        v-lazy:background-image="'./images/products/single/extended/bg-1.jpg'"
                    >
                        <div class="container">
                            <div class="row justify-content-end">
                                <div class="col-sm-6 col-lg-4">
                                    <h2>Product Information</h2>
                                    <ul>
                                        <li>Faux suede fabric upper</li>
                                        <li>Tie strap buckle detail</li>
                                        <li>Block heel</li>
                                        <li>Open toe</li>
                                        <li>Heel Height: 7cm / 2.5 inches</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div
                        class="product-desc-row bg-image text-white"
                        v-lazy:background-image="'./images/products/single/extended/bg-2.jpg'"
                    >
                        <div class="container">
                            <div class="row">
                                <div class="col-md-6">
                                    <h2>Design</h2>
                                    <p
                                        class="mb-2 mb-md-0"
                                    >The perfect choice for the summer months. These wedges are perfect for holidays and home, with the thick cross-over strap design and heel strap with an adjustable buckle fastening. Featuring chunky soles with an espadrille and cork-style wedge.</p>
                                </div>

                                <div class="col-md-6">
                                    <h2>Fabric & care</h2>
                                    <p>As part of our Forever Comfort collection, these wedges have ultimate cushioning with soft padding and flexi soles. Perfect for strolls into the old town on holiday or a casual wander into the village.</p>
                                </div>
                            </div>

                            <div class="mb-5"></div>

                            <img
                                v-lazy="'./images/products/single/extended/sign.png'"
                                alt="sign"
                                class="ml-auto mr-auto bg-transparent"
                            />
                        </div>
                    </div>

                    <div
                        class="product-desc-row bg-image"
                        v-lazy:background-image="'./images/products/single/extended/bg-3.jpg'"
                    >
                        <div class="container">
                            <div class="row">
                                <div class="col-lg-5">
                                    <blockquote>
                                        <p>
                                            “ Everything is important -
                                            <br />that success is in the details. ”
                                        </p>

                                        <cite>– Steve Jobs</cite>
                                    </blockquote>
                                    <p>Nullam mollis. Ut justo. Suspendisse potenti. Sed egestas, ante et vulputate volutpat, eros pede semper est, vitae luctus metus libero eu augue.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="tab-pane fade" id="tab2">
                <div class="product-desc-content">
                    <div class="container">
                        <h3>Information</h3>
                        <p
                            class="mb-2"
                        >Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Donec odio. Quisque volutpat mattis eros. Nullam malesuada erat ut turpis. Suspendisse urna viverra non, semper suscipit, posuere a, pede. Donec nec justo eget felis facilisis fermentum. Aliquam porttitor mauris sit amet orci.</p>

                        <h3>Fabric & care</h3>
                        <ul>
                            <li>Faux suede fabric</li>
                            <li>Gold tone metal hoop handles.</li>
                            <li>RI branding</li>
                            <li>Snake print trim interior</li>
                            <li>Adjustable cross body strap</li>
                            <li>Height: 31cm; Width: 32cm; Depth: 12cm; Handle Drop: 61cm</li>
                        </ul>

                        <h3>Size</h3>
                        <p>one size</p>
                    </div>
                </div>
            </div>

            <div class="tab-pane fade" id="tab3">
                <div class="product-desc-content">
                    <div class="container">
                        <h3>Delivery & returns</h3>
                        <p>
                            We deliver to over 100 countries around the world. For full details of the delivery options we offer, please view our
                            <a
                                href="#"
                            >Delivery information</a>
                            <br />We hope you’ll love every purchase, but if you ever need to return an item you can do so within a month of receipt. For full details of how to make a return, please view our
                            <a
                                href="#"
                            >Returns information</a>
                        </p>
                    </div>
                </div>
            </div>

            <div class="tab-pane fade" id="tab4">
                <div class="reviews">
                    <div class="container">
                        <h3>Reviews (2)</h3>
                        <div class="review">
                            <div class="row no-gutters">
                                <div class="col-auto">
                                    <h4>
                                        <a href="#">Samanta J.</a>
                                    </h4>
                                    <div class="ratings-container">
                                        <div class="ratings">
                                            <div class="ratings-val" style="width: 80%;"></div>
                                            <span class="tooltip-text">4</span>
                                        </div>
                                    </div>

                                    <span class="review-date">6 days ago</span>
                                </div>

                                <div class="col">
                                    <h4>Good, perfect size</h4>

                                    <div class="review-content">
                                        <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Ducimus cum dolores assumenda asperiores facilis porro reprehenderit animi culpa atque blanditiis commodi perspiciatis doloremque, possimus, explicabo, autem fugit beatae quae voluptas!</p>
                                    </div>

                                    <div class="review-action">
                                        <a href="#">
                                            <i class="icon-thumbs-up"></i>Helpful (2)
                                        </a>
                                        <a href="#">
                                            <i class="icon-thumbs-down"></i>Unhelpful (0)
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="review border-0">
                            <div class="row no-gutters">
                                <div class="col-auto">
                                    <h4>
                                        <a href="#">John Doe</a>
                                    </h4>
                                    <div class="ratings-container">
                                        <div class="ratings">
                                            <div class="ratings-val" style="width: 100%;"></div>
                                            <span class="tooltip-text">5</span>
                                        </div>
                                    </div>

                                    <span class="review-date">5 days ago</span>
                                </div>

                                <div class="col">
                                    <h4>Very good</h4>

                                    <div class="review-content">
                                        <p>Sed, molestias, tempore? Ex dolor esse iure hic veniam laborum blanditiis laudantium iste amet. Cum non voluptate eos enim, ab cumque nam, modi, quas iure illum repellendus, blanditiis perspiciatis beatae!</p>
                                    </div>

                                    <div class="review-action">
                                        <a href="#">
                                            <i class="icon-thumbs-up"></i>Helpful (0)
                                        </a>
                                        <a href="#">
                                            <i class="icon-thumbs-down"></i>Unhelpful (0)
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Tabs from '~/components/elements/Tabs';
export default {
    components: {
        Tabs
    },
    data: function() {
        return {
            tabsData: [
                {
                    id: 'tab1',
                    title: 'Description',
                    active: true
                },
                {
                    id: 'tab2',
                    title: 'Additional Information'
                },
                {
                    id: 'tab3',
                    title: 'Shipping & Returns'
                },
                {
                    id: 'tab4',
                    title: 'Reviews(2)'
                }
            ]
        };
    }
};
</script>