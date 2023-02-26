<template>
    <div class="product-details-tab">
        <tabs class="nav-pills justify-content-center" :data="tabsData"></tabs>
        <div class="tab-content">
            <div class="tab-pane fade show active" id="tab1">
                <div
                    class="product-desc-content blog_content_item"
                    v-html="getCurrentItem.detail_desc"
                    v-if="getCurrentItem"
                ></div>
            </div>

            <div class="tab-pane fade" id="tab2">
                <div class="product-desc-content blog_content_item">
                    <p>
                        <span style=""
                            >Khi mua sản phẩm, khách hàng có quyền hoàn toàn yên
                            tâm với sản phẩm chính hãng, được bảo hành chính
                            thức tại hãng và ngoài ra có thêm chính sách đổi mới
                            miễn phí lên tới 30 ngày.</span
                        >
                    </p>
                    <p>
                        <span style=""
                            >(*): Lỗi phần cứng từ phía nhà sản xuất</span
                        >
                    </p>
                    <p class="mb-2">
                        <span style="text-decoration: underline;"
                            ><em
                                ><strong
                                    ><span style=""
                                        >Điều kiện đổi trả:</span
                                    ></strong
                                ></em
                            ></span
                        >
                    </p>
                    <p>
                        <span
                            >● Máy: Như mới, không xước xát, không dán decal,
                            hình trang trí</span
                        ><br /><span
                            >● Máy cũ: có tình trạng sản phẩm như lúc mới
                            mua</span
                        ><br /><span
                            >● Hộp: Như mới, không móp méo, rách, vỡ, bị viết,
                            vẽ, quấn băng dính, keo; có Serial/IMEI trên hộp
                            trùng với thân máy.</span
                        ><br /><span
                            >● Phụ kiện và quà tặng: Còn đầy đủ, nguyên vẹn,
                            không móp méo xước xát hoặc bị hư hại trong quá
                            trình sử dụng.</span
                        ><br /><span
                            >● Tài khoản: Máy đã đã được đăng xuất khỏi tất cả
                            các tài khoản như: iCloud, Google Account, Mi
                            Account…</span
                        >
                    </p>
                    <p>
                        <span
                            >Các lỗi từ NSX cần được xác định bởi trung tâm bảo
                            hành chính hãng hoặc trung tâm bảo hành uỷ
                            quyền</span
                        >
                    </p>
                    <p>
                        <span
                            >(*) Lỗi từ phía Nhà sản xuất là các lỗi bao gồm:
                            Lỗi nguồn, lỗi trên mainboard, ổ cứng, màn hình và
                            các linh kiện phần cứng bên trong</span
                        >
                    </p>
                    <p>
                        <span
                            >Lỗi điểm chết màn hình : màn hình có từ 3 điểm chết
                            trở lên hoặc 1 điểm chết có kích thước lớn hơn 1mm
                            đối với điện thoại và từ 5 điểm chết trở lên đối với
                            laptop, màn hình rời</span
                        >
                    </p>
                </div>
            </div>

            <div class="tab-pane fade" id="tab3">
                <div class="reviews" style="text-align:center">
                    Chưa có dữ liệu
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Tabs from '~/components/elements/Tabs';
import { mapGetters } from 'vuex';

export default {
    components: {
        Tabs
    },
    computed: {
        ...mapGetters('product', ['getCurrentItem'])
    },
    data: function() {
        return {
            reviews: [],
            tabsData: [
                {
                    id: 'tab1',
                    title: 'Thông tin sản phẩm',
                    active: true
                },
                {
                    id: 'tab2',
                    title: 'Chính sách khách hảng'
                },
                {
                    id: 'tab3',
                    title: 'Bình luận (0)'
                }
            ]
        };
    }
};
</script>

<style lang="scss">
.product-desc-content {
    img {
        margin: 15px auto 20px auto;
    }
    h3,
    h4,
    h5 {
        margin-top: 20px;
    }
}
</style>
